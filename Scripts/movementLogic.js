function movementLogic()
{
    if(player.left)
    {
      player.x -= player.speed;
    }
    if(player.right)
    {
      player.x += player.speed;
    }
    if(player.up)
    {
      player.y -= player.speed;
    }
    if(player.down)
    {
      player.y += player.speed;
    }
}

function keydown(event)
{
    switch(event.keyCode)
    {
        case 37: player.left = true; break;
        case 38: player.up = true; break;
        case 39: player.right = true; break;
        case 40: player.down = true; break;
        case 90: player.fire = true; break;
    }
}

function keyup(event)
{
    switch(event.keyCode)
    {
        case 37: player.left = false; break;
        case 38: player.up = false; break;
        case 39: player.right = false; break;
        case 40: player.down = false; break;
        case 90: player.fire = false; break;
    }
}