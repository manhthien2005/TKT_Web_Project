document.addEventListener('DOMContentLoaded', function () {
    const slider = document.querySelector('.slider');
    const pagination = document.querySelectorAll('.pagination .page');
    let translateX = 0;
    const step = 1137;
    const ProPrevBtn = document.getElementById('prevBtn');
    const ProNextBtn = document.getElementById('nextBtn');
    let currentPage = 0;

    function handleSliderMovement(direction) {
        if (direction === 'next') {
            translateX -= step;
            currentPage++;
        } else if (direction === 'prev') {
            translateX += step;
            currentPage--;
        }

        if (translateX == -3411) {
            slider.style.transform = `translateX(0px)`;
            translateX = 0;
            currentPage = 0;
        }

        if (translateX < 0) {
            ProPrevBtn.style.visibility = "visible";
        } else {
            ProPrevBtn.style.visibility = "hidden";
        }

        // Update pagination
        updatePagination();

        slider.style.transform = `translateX(${translateX}px)`;
    }

    function updatePagination() {
        // Reset all pagination dots
        pagination.forEach((page, index) => {
            if (index === currentPage) {
                page.style.background = "rgb(10, 104, 255)";  // Active color
                page.style.width = "24px";
            } else {
                page.style.background = "rgba(0, 0, 0, 0.05)";  // Inactive color
                page.style.width = "16px";
            }
        });
    }

    ProPrevBtn.addEventListener('click', function () {
        handleSliderMovement('prev');
    });

    ProNextBtn.addEventListener('click', function () {
        handleSliderMovement('next');
    });

    // Initial pagination update
    updatePagination();
});
