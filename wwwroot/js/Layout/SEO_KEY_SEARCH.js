const input = document.querySelector('.search_input');
const phrases = ["Tìm kiếm thứ gì", "Điện thoại giá rẻ", "Sữa trẻ em"];
let phraseIndex = 0;
let charIndex = 0;

function typeEffect() {
    if (charIndex < phrases[phraseIndex].length) {
        input.placeholder += phrases[phraseIndex][charIndex];
        charIndex++;
        setTimeout(typeEffect, 100); // Thời gian chờ cho mỗi ký tự
    } else {
        setTimeout(deleteEffect, 3000); // Thời gian chờ trước khi xóa
    }
}

function deleteEffect() {
    if (charIndex > 0) {
        input.placeholder = input.placeholder.slice(0, --charIndex);
        setTimeout(deleteEffect, 50); // Thời gian chờ cho mỗi ký tự bị xóa
    } else {
        phraseIndex = (phraseIndex + 1) % phrases.length; // Chuyển sang cụm từ tiếp theo
        setTimeout(typeEffect, 100); // Thời gian chờ trước khi gõ lại
    }
}

// Bắt đầu hiệu ứng khi tải trang
window.addEventListener("load", () => {
    input.placeholder = ""; // Xóa placeholder ban đầu
    typeEffect();
});
