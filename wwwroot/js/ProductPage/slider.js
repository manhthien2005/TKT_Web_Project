document.addEventListener('DOMContentLoaded', function () {
    const slider = document.querySelector('.slider');
    const width = slider.offsetWidth;
    
    let translateX = 0;
    const step = 372;
    const result = Math.ceil(width / 372);
    const ProPrevBtn = document.getElementById('prevbtn');
    const ProNextBtn = document.getElementById('nextbtn');

    function handleSliderMovement(direction) {
        let tmp = 1;

        if (direction === 'next') {
            translateX -= step;
            tmp += 1;

            if (tmp == result)
                ProNextBtn.style.visibility = "hidden";
            else
                ProNextBtn.style.visibility = "visible";

            if (translateX == 0) {
                ProPrevBtn.style.visibility = "hidden";
            }
            else {
                ProPrevBtn.style.visibility = "visible";
            }
                
        } else if (direction === 'prev') {
            translateX += step;
            tmp -= 1;

            if (tmp == result)
                ProNextBtn.style.visibility = "hidden";
            else
                ProNextBtn.style.visibility = "visible";

            if (translateX == 0) {
                ProPrevBtn.style.visibility = "hidden";
            }
            else {
                ProPrevBtn.style.visibility = "visible";
            }
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
