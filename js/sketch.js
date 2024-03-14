const fs = require('fs');
const path = require('path');

fs.readdir('./scripts', (err, files) => {
 files.forEach(file => {
    if (path.extname(file) === '.js') {
      import(`./scripts/${file}`).then(module => {
        // Use the imported module here
        console.log(module);
      });
    }
 });
});

function setup() {
    createCanvas(800, 600);
    // Initialize game objects here
}

function draw() {
    background(0);
    // Game logic and rendering here
}
