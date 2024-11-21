document.addEventListener('DOMContentLoaded', function () {
    const slider = document.querySelector('.ProductSlider');
    let translateX = 0;
    const step = 946;
    const ProPrevBtn = document.getElementById('ProPrevBtn');
    const ProNextBtn = document.getElementById('ProNextBtn');

    function handleSliderMovement(direction) {
        if (direction === 'next') {
            translateX -= step;
        } else if (direction === 'prev') {
            translateX += step;
        }

        if (translateX == -2838) {
            slider.style.transform = `translateX(0px)`;
            translateX = 0;
        }

        if (translateX < 0) {
            ProPrevBtn.style.visibility = "visible";
        } else {
            ProPrevBtn.style.visibility = "hidden";
        }

        slider.style.transform = `translateX(${translateX}px)`;
    }

    ProPrevBtn.addEventListener('click', function () {
        handleSliderMovement('prev');
    });

    ProNextBtn.addEventListener('click', function () {
        handleSliderMovement('next');
    });
});
