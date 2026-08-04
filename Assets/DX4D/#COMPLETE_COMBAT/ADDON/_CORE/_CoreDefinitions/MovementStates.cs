///You can cast this to an (int) to get the move speed multiplier as a percentage.
public enum MoveState {
    //MOVEMENT
    IDLE = 100,
    STANDING = 100,
    WALKING = 100,
    RUNNING = 150,
    SPRINTING = 200,
    //SKILL RELATED
    SNEAKING = 50,
    CHARGING = 200,
    //INJURIES
    IMMOBILIZED = 0,
    WOUNDED = 20,
    EXHAUSTED = 40,
    //MANEUVERS
    CRAWLING = 20,
    CROUCHING = 30,
    CLIMBING = 60,
    JUMPING = 90,
    SWIMMING = 60,
    FLYING = 500,
    WARPING = 1000
}
