function initializeCanvas()
{
  canvas = document.createElement('canvas');
  canvas.width = 32 * 12;
  canvas.height = 32 * 12;
  document.body.appendChild(this.canvas);

  this.context = this.canvas.getContext('2d');
  this.context.imageSmoothingEnabled = false;
  this.context.scale(1, 1);
}

function paintScreen()
{
  context.fillStyle = "#000";
  context.fillRect(0, 0, canvas.width, canvas.height);
}