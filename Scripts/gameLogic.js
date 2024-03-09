const FPS = 30;

window.onload = function()
{
  initializeCanvas();
  initializePlayer();
  document.addEventListener("keydown", keydown);
  document.addEventListener("keyup", keyup);
  setInterval(gameLogic, 1000 / FPS);
}

function gameLogic()
{
    movementLogic();
    paintScreen();
    animate();
}