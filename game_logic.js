
var x = 100;
var y = 100;

var left = false;
var up = false;
var right = false;
var down = false;

var canvas = document.getElementById("game");
var ctx = canvas.getContext("2d");
var fps = 26;

var playerSpeed = 13;

window.onload = function()
{
    document.addEventListener("keydown", keydown);
    document.addEventListener("keyup", keyup);
    setInterval(gameloop, 1000 / fps);
}

function gameloop()
{
    gamelogic();
    paintScreen();
}

function intersectRect(r1l, r1u, r1r, r1d, r2l, r2u, r2r, r2d)
{
    return !(r2l > r1r || r2r < r1l || r2u > r1d || r2d < r1u)
}

function isColliding(potentialX, potentialY, playerWidth, playerHeight)
{
    let isColliding = false
    walls.forEach((wall) => 
    {
        if(intersectRect(
            potentialX, 
            potentialY,
            potentialX + playerWidth,
            potentialY + playerHeight,
            wall[0],
            wall[1],
            wall[0] + wall[2],
            wall[1] + wall[3]))
        {
            isColliding = true;
            return;
        }
    });

    return isColliding;
}

function gamelogic()
{
    if(left && !isColliding(player[0] - playerSpeed, player[1], player[0] + player[2], player[1] + player[3]))
    {
        x -= playerSpeed;
    }
    if(right)
    {
        if(!isColliding(player))
        {
            x += playerSpeed;
        }
    }
    if(up)
    {
        if(!isColliding(player))
        {
            y -= playerSpeed;
        }
    }
    if(down)
    {
        if(!isColliding(player))
        {
            y += playerSpeed;
        }
    }
}

function keydown(event)
{
    console.log(event.keyCode)
    switch(event.keyCode)
    {
        case 37: left = true; break;
        case 38: up = true; break;
        case 39: right = true; break;
        case 40: down = true; break;
    }
}

function keyup(event)
{
    switch(event.keyCode)
    {
        case 37: left = false; break;
        case 38: up = false; break;
        case 39: right = false; break;
        case 40: down = false; break;
    }
}

var player = [ 0, 0, 100, 100 ]
var wall1 = [ 50, 50, 50, 500 ]
var wall2 = [ 50, 50, 500, 50 ]
var wall3 = [ 500, 50, 50, 500 ]
var wall4 = [ 50, 500, 500, 50 ]
var walls = [ wall1, wall2, wall3, wall4 ]

function paintScreen()
{
    ctx.fillStyle = "#000";
    ctx.fillRect(0, 0, canvas.width, canvas.height);

    ctx.fillStyle = "#696";
    player[0] = x;
    player[1] = y;
    ctx.fillRect(player[0], player[1], player[2], player[3]);

    walls.forEach((wall) => 
    {
        ctx.fillStyle = "#fc031c";
        ctx.fillRect(wall[0], wall[1], wall[2], wall[3]);
    })
}