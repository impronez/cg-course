window.onload = main

function main() {
    const firstLetter = document.getElementById('fst-letter');
    const secondLetter = document.getElementById('snd-letter');
    const thirdLetter = document.getElementById('trd-letter');

    createDropAnimation(firstLetter, 500);
    createDropAnimation(secondLetter, 1000);
    createDropAnimation(thirdLetter, 1500);
}

function createDropAnimation(element, delay) { 
    const gravity = 100;
    const startTime = Date.now() + delay;
    const maxHeight = 500;
    let currentY = 0;
    let prevTime = null;
    let speed = 0;

    function updatePosition() {
        const currentTime = Date.now();
        
        if (currentTime < startTime) {
            requestAnimationFrame(updatePosition);
            return;
        }

        if (!prevTime) {
            prevTime = currentTime;
        }

        const deltaTime = (currentTime - prevTime) / 1000;
        
        speed += gravity * deltaTime;

        const deltaY = speed * deltaTime;

        currentY += deltaY;
        currentY = Math.min(currentY, maxHeight);

        element.style.transform = `translateY(${currentY}px)`;

        if (currentY >= maxHeight) {
            speed = -speed;
            currentY = maxHeight;
        }

        prevTime = currentTime;
        requestAnimationFrame(updatePosition);
    }

    requestAnimationFrame(updatePosition);
}
