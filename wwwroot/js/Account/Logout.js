function logoutUser() {
    fetch('/Account/Logout', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        credentials: 'same-origin' // Đảm bảo cookie được gửi cùng với yêu cầu
    })
        .then(response => {
            if (response.ok) {
                window.location.href = '/Account/Login'; // Điều hướng đến trang đăng nhập
            } else {
                alert('Đăng xuất không thành công');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}
