document.addEventListener("DOMContentLoaded", function () {
    const decreaseButton = document.getElementById("decrease");
    const increaseButton = document.getElementById("increase");
    const quantityInput = document.getElementById("quantity");
    const tempPriceDiv = document.getElementById("tempPrice");


    function updateButtons() {
        const value = parseInt(quantityInput.value, 10);
        decreaseButton.disabled = value <= 1;
    }

    function updateFinalPrice() {
        const quantity = parseInt(quantityInput.value, 10);
        const tempPrice = finalPrice * quantity;
        tempPriceDiv.innerHTML = `${tempPrice.toLocaleString()}<sup>₫</sup>`; // Định dạng VND
    }

    increaseButton.addEventListener("click", () => {
        quantityInput.value = parseInt(quantityInput.value, 10) + 1;
        updateButtons();
        updateFinalPrice();
    });

    decreaseButton.addEventListener("click", () => {
        const currentValue = parseInt(quantityInput.value, 10);
        if (currentValue > 1) {
            quantityInput.value = currentValue - 1;
            updateButtons();
            updateFinalPrice();
        }
    });

    updateButtons();
    updateFinalPrice();
});