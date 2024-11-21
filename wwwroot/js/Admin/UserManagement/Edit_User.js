// Mở modal chỉnh sửa thông tin người dùng
function openEditModal(userID) {

    document.getElementById("editModal").style.display = "block";
    console.log(userID);

    fetch(`/Admin/GetUserDetails?userID=${userID}`)
        .then(response => response.json())
        .then(user => {
            console.log("User data received:", user);

            document.getElementById("editUsername").value = user.userID;
            document.getElementById("editUsername").value = user.username || '';
            document.getElementById("editName").value = user.name || '';
            document.getElementById("editPhone").value = user.phone || '';
            document.getElementById("editEmail").value = user.email || '';
            document.getElementById("editAddress").value = user.address || '';
            document.getElementById("editBirthday").value = user.birthday || '';
            document.getElementById("editNationality").value = user.nationality || '';
            document.getElementById("editRole").value = user.role || 0;
            document.getElementById("editGender").value = user.gender || 'Male';
        })
        .catch(error => {
            console.error("Error fetching user data:", error);
        });
}

// Đóng modal chỉnh sửa
function closeEditModal() {
    document.getElementById("editModal").style.display = "none";
}

function saveUserChanges() {
    const updatedUser = {
        userID: document.getElementById('userID').value,
        name: document.getElementById('userName').value,
        email: document.getElementById('userEmail').value,
        phone: document.getElementById('userPhone').value
    };
    fetch('/Admin/UpdateUserDetails', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updatedUser)
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert(data.message);
                closeEditModal();
                location.reload(); // Reload trang để cập nhật thông tin
            } else {
                alert("Update failed: " + data.message);
            }
        })
        .catch(error => {
            console.error("Error updating user:", error);
            alert("An error occurred while updating the user.");
        });
}