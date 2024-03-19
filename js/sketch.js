let player;
let cells = [];
function setup() {
    createCanvas(700, 700);
    player = new Player();
}

function draw() {
    background(100, 100, 100);
    rectMode(CENTER);
    player.draw();
    player.update();

  for (let i = cells.length - 1; i >= 0; i--) {
    cells[i].draw();
    cells[i].update();
  }

  player.hasShot(cells);

  if (frameCount % 50 == 0) {
    cells.push(new Cell(2));
  }

  if (frameCount % 100 == 0) {
    cells.push(new CellSwam(2));
  }
}

class Player {
    constructor() {
        this.pos = createVector(width / 2, height / 2);
        this.bullets = [];
    }

    draw() {
        push();
        translate(this.pos.x, this.pos.y);
        rect(0, 0, 20, 20);
        pop();

        for (let i = 0; i < this.bullets.length; i++) {
            this.bullets[i].update();
            this.bullets[i].draw();
            if (this.bullets[i].y < -100) {
                this.bullets.splice(i, 1);
              }
          }
    }

    update() {
        let xSpeed = 0;
        let ySpeed = 0;
        if (keyIsDown(37)) {
          xSpeed = -2;
        }
    
        if (keyIsDown(39)) {
          xSpeed = 2;
        }
    
        if (keyIsDown(38)) {
          ySpeed = -2;
        }
    
        if (keyIsDown(40)) {
          ySpeed = 2;
        }

        if (keyIsDown(90)) {
            this.shoot();
          }

        this.pos.add(xSpeed, ySpeed);
    }

    shoot() {
        if(this.bullets.length > 1) return;
        this.bullets.push(new Bullet(this.pos.x, this.pos.y));
    }

    hasShot(cells) {
        for (let i = cells.length - 1; i >= 0; i--) {
          if (cells[i].hasBeenShot(this.bullets)) {
            cells.splice(i, 1);
          }
        }
      }
}

class Cell {
  constructor(speed, posX = null, posY = null) {
      this.speed = speed;
      let y = posY;
      if(posY == null) {
        if (random(1) < 0.5) {
          // from the top
          y = random(-300, 0);            
        }
      }

      let x = posX;
      if(posY == null) {
        x = random(0, width);
      }
      
      this.pos = createVector(x, y);
  }

  draw() {
      push();
      fill(100, 255, 100);
      rect(this.pos.x, this.pos.y, 20, 20);
      pop();
  }
  
  
  update() {
      let difference = p5.Vector.sub(player.pos, this.pos);
      difference.limit(this.speed);
      this.pos.add(difference);
  }

  hasBeenShot(bullets) {
    for (let i = 0; i < bullets.length; i++) {
      if(dist(bullets[i].x, bullets[i].y, this.pos.x, this.pos.y) < 15)
      {
        bullets.splice(i, 1);
        return true;
      }
    }

    return false;
  }
}

class CellSwam extends Cell {
  constructor(speed, posX = null, posY = null) {
    super(speed, posX, posY);
    this.cells = [];
    this.cells.push(new Cell(0, this.pos.x, this.pos.y));
    this.cells.push(new Cell(0, this.pos.x + 20, this.pos.y));
    this.cells.push(new Cell(0, this.pos.x, this.pos.y + 20));
    this.cells.push(new Cell(0, this.pos.x + 20, this.pos.y + 20));
  }

  draw() {
    push();
    fill(100, 255, 100);
    for (let i = this.cells.length - 1; i >= 0; i--) {
      rect(this.cells[i].pos.x, this.cells[i].pos.y, 20, 20);
    }
    pop();
  }

  update() {
    super.update();

    if(typeof this.cells[0] !== 'undefined')
    {
      this.cells[0].pos = createVector(this.pos.x, this.pos.y);
    }

    if(typeof this.cells[1] !== 'undefined')
    {
      this.cells[1].pos = createVector(this.pos.x + 20, this.pos.y);
    }

    if(typeof this.cells[2] !== 'undefined')
    {
      this.cells[2].pos = createVector(this.pos.x, this.pos.y + 20);
    }

    if(typeof this.cells[3] !== 'undefined')
    {
      this.cells[3].pos = createVector(this.pos.x + 20, this.pos.y + 20);
    }
  }

  hasBeenShot(bullets) {
    for (let i = this.cells.length - 1; i >= 0; i--) {
      if(this.cells[i].hasBeenShot(bullets))
      {
        this.cells.splice(i, 1);
      }
    }

    return this.cells.length == 0
  }
}

class Bullet {
    constructor(x, y) {
      this.x = x;
      this.y = y;
      this.speed = 16;
    }
    
    draw() {
      push();
      fill(0);
      circle(this.x, this.y, 5);
      pop();
    }
    
    update() {
      this.y -= this.speed;
    }
  }
