var player;

function initializePlayer()
{
    player = 
    {
    x: 100,
    y: 100,
    left: false,
    up: false,
    right: false,
    down: false,
    fire: false,
    speed: 8,
    frame: 0,
    frameIndex: 0,
    getSpriteSheet: function ()
        {
        let image = new Image();
        image.src = "Assets/Player_Ship_1.png";
        image.crossOrigin = true;
        return image;
        },
        currentSprite: 0,
        flightCycle: [
        new SpriteFrameLocation
            (
            0 * 32,
            0 * 32,
            32,
            32
            ),
        new SpriteFrameLocation
            (
            0 * 32,
            1 * 32,
            32,
            32
            ),
        ]
    };
}

function animate()
{
    if(player.frameIndex == player.flightCycle.length)
    {
        player.frameIndex = 0;
    }

    player.frame = player.flightCycle[player.frameIndex];

    context.drawImage(
        player.getSpriteSheet(),
        player.frame.x,
        player.frame.y,
        player.frame.spriteWidth,
        player.frame.spriteHeight,
        player.x,
        player.y,
        player.frame.spriteWidth,
        player.frame.spriteHeight,
    );
    player.frameIndex += 1;
}