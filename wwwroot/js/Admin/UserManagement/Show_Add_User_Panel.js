// Hàm để hiển thị panel
function showUserPanel() {
    // Hiển thị panel và overlay
    document.getElementById("user-panel").style.display = "block";
    document.getElementById("user-panel-overlay").style.display = "block";

    // Đặt ngày tạo mặc định là ngày hiện tại
    const currentDate = new Date().toISOString().split('T')[0]; // Lấy ngày ở định dạng YYYY-MM-DD
    document.getElementById("create_at").value = currentDate;
}

function closeUserPanel() {
    // Ẩn panel và overlay
    document.getElementById("user-panel").style.display = "none";
    document.getElementById("user-panel-overlay").style.display = "none";

    form.reset();
    document.getElementById('success-messages').innerHTML = '';
}


// Đóng panel khi nhấn phím "Esc"
window.addEventListener("keydown", function (event) {
    if (event.key === "Escape") {
        closeUserPanel();
    }
});


document.getElementById("avatar").addEventListener("change", function (event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            // Hiển thị ảnh thumbnail nếu cần
            const avatarPreview = document.createElement("img");
            avatarPreview.src = e.target.result;
            avatarPreview.style.width = "100px"; // Điều chỉnh kích thước ảnh
            avatarPreview.style.height = "100px";
            avatarPreview.style.borderRadius = "50%";
            document.getElementById("avatar").parentNode.appendChild(avatarPreview);
        };
        reader.readAsDataURL(file);
    }
});


