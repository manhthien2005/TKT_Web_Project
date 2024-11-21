document.querySelectorAll('.ThumbnailItemStyle').forEach(thumbnailItem => {
    const thumbnailImage = thumbnailItem.querySelector('img');

    thumbnailImage.addEventListener('mouseover', function () {

        const mainImage = document.querySelector('#mainImage img');


        mainImage.src = this.src;


        document.querySelectorAll('.ThumbnailItemStyle').forEach(item => {
            item.style.border = '1px solid rgb(235, 235, 240)';
        });


        thumbnailItem.style.border = '2px solid rgb(0, 0, 255)';
    });
});
