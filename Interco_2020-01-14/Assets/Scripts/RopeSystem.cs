using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RopeSystem : MonoBehaviour
{
        public LineRenderer ropeRenderer;
        public LayerMask ropeLayerMask;
        public float climbSpeed = 3f;
        public GameObject ropeHingeAnchor;
        public DistanceJoint2D ropeJoint;
        public Transform crosshair;
        public SpriteRenderer crosshairSprite;
        public PlayerMovement playerMovement;
        private bool ropeAttached;
        private Vector2 playerPosition;
        private List<Vector2> ropePositions = new List<Vector2>();
        private float ropeMaxCastDistance = 20f;
        private Rigidbody2D ropeHingeAnchorRb;
        private bool distanceSet;
        private bool isColliding;
        private Dictionary<Vector2, int> wrapPointsLookup = new Dictionary<Vector2, int>();
        private SpriteRenderer ropeHingeAnchorSprite;
        public bool instant = false;
        public float bulletSpeed = 1f;
        public float ropeLength = 100f;
        public bool climbable = false;

        private bool shooting = false;
        private Vector2 bulletPosition;
        private Vector2 bulletDirection;

    //Audio attr
    public AudioSource AudioSource;
    public AudioClip[] HookClips;
    public AudioClip SwingingClip;

    void Awake()
        {
                ropeJoint.enabled = false;
                playerPosition = transform.position;
                ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
                ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
        }

        private Vector2 GetClosestColliderPoint(Vector2 point, PolygonCollider2D polyCollider)
        {
                var distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
                    position => Vector2.Distance(point, polyCollider.transform.TransformPoint(position)),
                    position => polyCollider.transform.TransformPoint(position));

                var orderedDictionary = distanceDictionary.OrderBy(e => e.Key);
                return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;
        }

        private void ProcessBullet()
        {
                bulletPosition += bulletDirection * bulletSpeed;
                ropePositions.Clear();

                //transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                ropePositions.Add(bulletPosition);
                wrapPointsLookup.Add(bulletPosition, 0);
                ropeJoint.distance = Vector2.Distance(playerPosition, bulletPosition);
                //ropeJoint.enabled = true;
                ropeHingeAnchorSprite.enabled = true;

                var bulletToCurrentNextHit = Physics2D.Raycast(playerPosition, (bulletPosition - playerPosition).normalized, Vector2.Distance(playerPosition, bulletPosition) - 0.1f, ropeLayerMask);
                if (bulletToCurrentNextHit)
                {
                        var colliderWithVertices = bulletToCurrentNextHit.collider as PolygonCollider2D;
                        if (colliderWithVertices != null)
                        {
                                var closestPointToHit = GetClosestColliderPoint(bulletToCurrentNextHit.point, colliderWithVertices);
                                if (Vector2.Distance(closestPointToHit, bulletPosition) <= bulletSpeed * 2f)
                                {
                                        shooting = false;
                                        ropeJoint.enabled = true;
                                }
                        }
                }
        }

        private void CancelShooting()
        {
                AudioSource.Stop();
                shooting = false;
                ropePositions.Clear();
                ropeRenderer.enabled = false;
                ropeAttached = false;
                ropeJoint.enabled = false;
                ResetRope();
        }


        void Update()
        {
                if (shooting)
                {
                        if (Vector2.Distance(playerPosition, bulletPosition) > ropeLength)
                        {
                                print("f");
                                CancelShooting();
                        }
                        else
                        {
                                ProcessBullet();
                        }
                }

                var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
                var facingDirection = worldMousePosition - transform.position;
                var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
                if (aimAngle < 0f)
                {
                        aimAngle = Mathf.PI * 2 + aimAngle;
                }

                var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
                playerPosition = transform.position;

                if (!ropeAttached)
                {
                        SetCrosshairPosition(aimAngle);
                        playerMovement.isSwinging = false;
                }
                else
                {
                        playerMovement.isSwinging = true;
                        playerMovement.ropeHook = ropePositions.Last();
                        crosshairSprite.enabled = false;

                        if (ropePositions.Count > 0)
                        {
                                var lastRopePoint = ropePositions.Last();
                                var playerToCurrentNextHit = Physics2D.Raycast(playerPosition, (lastRopePoint - playerPosition).normalized, Vector2.Distance(playerPosition, lastRopePoint) - 0.1f, ropeLayerMask);
                                if (playerToCurrentNextHit)
                                {
                                        var colliderWithVertices = playerToCurrentNextHit.collider as PolygonCollider2D;
                                        if (colliderWithVertices != null)
                                        {
                                                var closestPointToHit = GetClosestColliderPoint(playerToCurrentNextHit.point, colliderWithVertices);
                                                if (wrapPointsLookup.ContainsKey(closestPointToHit))
                                                {
                                                        ResetRope();
                                                        return;
                                                }

                                                ropePositions.Add(closestPointToHit);
                                                wrapPointsLookup.Add(closestPointToHit, 0);
                                                distanceSet = false;
                                        }
                                }
                        }
                }

                UpdateRopePositions();
                if (climbable)
                {
                        HandleRopeLength();
                }
                HandleInput(aimDirection);
        }

        private void InstantHookAttach(Vector2 aimDirection)
        {
                var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);
                if (hit.collider != null)
                {
                        ropeAttached = true;
                        if (!ropePositions.Contains(hit.point))
                        {

                                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                                ropePositions.Add(hit.point);
                                wrapPointsLookup.Add(hit.point, 0);
                                ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                                ropeJoint.enabled = true;
                                ropeHingeAnchorSprite.enabled = true;
                        }
                }
                else
                {
                        ropeRenderer.enabled = false;
                        ropeAttached = false;
                        ropeJoint.enabled = false;
                }
        }

        private void GunHookShoot(Vector2 aimDirection)
        {
                shooting = true;
                bulletDirection = aimDirection;
                bulletPosition = playerPosition;
                ropeAttached = true;
                AudioSource.PlayOneShot(HookClips[Random.Range(0, 2)]);
                AudioSource.PlayOneShot(SwingingClip);
                return;

                //var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);
                //if (hit.collider != null)
                //{
                //        ropeAttached = true;
                //        /*
                //        if (!ropePositions.Contains(hit.point))
                //        {
                //                //transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                //                //ropePositions.Add(hit.point);
                //                //wrapPointsLookup.Add(hit.point, 0);
                //                //ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                //                //ropeJoint.enabled = true;
                //                //ropeHingeAnchorSprite.enabled = true;
                //        }
                //        */
                //}
                //else
                //{
                //        ropeRenderer.enabled = false;
                //        ropeAttached = false;
                //        ropeJoint.enabled = false;
                //}

        

        }

        private void HandleInput(Vector2 aimDirection)
        {
                if (Input.GetMouseButton(0))
                {
                        if (ropeAttached) return;
                        ropeRenderer.enabled = true;

                        if (instant)
                        {
                                InstantHookAttach(aimDirection);
                        }
                        else
                        {
                                GunHookShoot(aimDirection);
                        }
                }

                if (Input.GetMouseButton(1))
                {
            AudioSource.Stop();
                        ResetRope();
                }
        }

        /// <summary>
        /// Resets the rope in terms of gameplay, visual, and supporting variable values.
        /// </summary>
        private void ResetRope()
        {
                ropeJoint.enabled = false;
                ropeAttached = false;
                playerMovement.isSwinging = false;
                ropeRenderer.positionCount = 2;
                ropeRenderer.SetPosition(0, transform.position);
                ropeRenderer.SetPosition(1, transform.position);
                ropePositions.Clear();
                wrapPointsLookup.Clear();
                ropeHingeAnchorSprite.enabled = false;
        }

        /// <summary>
        /// Move the aiming crosshair based on aim angle
        /// </summary>
        /// <param name="aimAngle">The mouse aiming angle</param>
        private void SetCrosshairPosition(float aimAngle)
        {
                if (!crosshairSprite.enabled)
                {
                        crosshairSprite.enabled = true;
                }

                var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
                var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

                var crossHairPosition = new Vector3(x, y, 0);
                crosshair.transform.position = crossHairPosition;
        }

        /// <summary>
        /// Retracts or extends the 'rope'
        /// </summary>
        private void HandleRopeLength()
        {
                if (Input.GetAxis("Vertical") >= 1f && ropeAttached && !isColliding)
                {
                        ropeJoint.distance -= Time.deltaTime * climbSpeed;
                }
                else if (Input.GetAxis("Vertical") < 0f && ropeAttached)
                {
                        ropeJoint.distance += Time.deltaTime * climbSpeed;
                }
        }

        /// <summary>
        /// Handles updating of the rope hinge and anchor points based on objects the rope can wrap around. These must be PolygonCollider2D physics objects.
        /// </summary>
        private void UpdateRopePositions()
        {
                if (ropeAttached)
                {
                        ropeRenderer.positionCount = ropePositions.Count + 1;

                        for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
                        {
                                if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
                                {
                                        ropeRenderer.SetPosition(i, ropePositions[i]);

                                        // Set the rope anchor to the 2nd to last rope position (where the current hinge/anchor should be) or if only 1 rope position then set that one to anchor point
                                        if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
                                        {
                                                if (ropePositions.Count == 1)
                                                {
                                                        var ropePosition = ropePositions[ropePositions.Count - 1];
                                                        ropeHingeAnchorRb.transform.position = ropePosition;
                                                        if (!distanceSet)
                                                        {
                                                                ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                                                                distanceSet = true;
                                                        }
                                                }
                                                else
                                                {
                                                        var ropePosition = ropePositions[ropePositions.Count - 1];
                                                        ropeHingeAnchorRb.transform.position = ropePosition;
                                                        if (!distanceSet)
                                                        {
                                                                ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                                                                distanceSet = true;
                                                        }
                                                }
                                        }
                                        else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                                        {
                                                // if the line renderer position we're on is meant for the current anchor/hinge point...
                                                var ropePosition = ropePositions.Last();
                                                ropeHingeAnchorRb.transform.position = ropePosition;
                                                if (!distanceSet)
                                                {
                                                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                                                        distanceSet = true;
                                                }
                                        }
                                }
                                else
                                {
                                        // Player position
                                        ropeRenderer.SetPosition(i, transform.position);
                                }
                        }
                }
        }

        void OnTriggerStay2D(Collider2D colliderStay)
        {
                isColliding = true;
        }

        private void OnTriggerExit2D(Collider2D colliderOnExit)
        {
                isColliding = false;
        }
}
