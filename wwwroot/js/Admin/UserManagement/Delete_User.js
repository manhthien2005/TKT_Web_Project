

// Khai báo các biến
let modal = document.getElementById("deleteModal");
let closeModal = document.getElementById("closeModal");
let cancelDelete = document.getElementById("cancelDelete");
let confirmDelete = document.getElementById("confirmDelete");
let userIdToDelete = null;

// Hiển thị modal khi nhấn nút "Xóa"
function setDeleteUserId(userId) {
    userIdToDelete = userId;
    modal.style.display = "flex"; // Hiển thị modal
}

// Ẩn modal khi nhấn nút đóng
closeModal.onclick = function () {
    modal.style.display = "none"; // Ẩn modal
}

// Hủy bỏ việc xóa và đóng modal
cancelDelete.onclick = function () {
    modal.style.display = "none"; // Ẩn modal
}

// Khi nhấn "Xóa", thực hiện xóa người dùng
confirmDelete.onclick = function () {
    if (userIdToDelete !== null) {
        // Gửi yêu cầu xóa người dùng
        fetch(`/Admin/DeleteUser/${userIdToDelete}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        })
            .then(response => {
                if (response.ok) {
                    location.reload(); // Reload trang sau khi xóa thành công
                } else {
                    alert('Lỗi khi xóa người dùng!');
                }
            })
            .catch(error => {
                alert('Có lỗi xảy ra!');
            });
    }
}
