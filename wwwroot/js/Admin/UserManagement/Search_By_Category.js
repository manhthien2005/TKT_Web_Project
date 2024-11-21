function toggleListDropdown() {
    const dropdown = document.getElementById("list-dropdown");

    // Toggle visibility and opacity
    if (dropdown.style.visibility === "visible") {
        dropdown.style.visibility = "hidden";
        dropdown.style.opacity = "0";
    } else {
        dropdown.style.visibility = "visible";
        dropdown.style.opacity = "1";
    }
}

// Close dropdown if clicked outside
document.addEventListener("click", function (event) {
    const dropdown = document.getElementById("list-dropdown");
    const listIcon = document.querySelector(".fa-list");

    // Check if the click was outside the dropdown or icon
    if (!dropdown.contains(event.target) && event.target !== listIcon) {
        dropdown.style.visibility = "hidden";
        dropdown.style.opacity = "0";
    }
});

function applySearch() {
    const searchBy = document.getElementById("search-by").value;  // Lấy giá trị chọn trong dropdown
    const searchInput = document.getElementById("search-bar").value.toLowerCase();  // Lấy giá trị người dùng gõ vào thanh tìm kiếm

    // Cập nhật placeholder của thanh tìm kiếm
    const searchBar = document.getElementById("search-bar");
    searchBar.placeholder = "Search by " + searchBy.charAt(0).toUpperCase() + searchBy.slice(1);

    const rows = document.querySelectorAll(".user-row");

    rows.forEach(row => {
        // Tìm thẻ có data-search tương ứng với giá trị trong dropdown
        const searchColumn = row.querySelector(`[data-search="${searchBy}"]`);

        if (searchColumn) {
            // Lấy nội dung văn bản trong thẻ tìm kiếm
            const textContent = searchColumn.textContent.trim().toLowerCase();

            // Kiểm tra nếu giá trị tìm kiếm khớp với nội dung
            if (textContent && textContent.includes(searchInput)) {
                row.style.display = "";  // Hiển thị dòng nếu khớp
            } else {
                row.style.display = "none";  // Ẩn dòng nếu không khớp
            }
        }
    });

    // Ẩn dropdown sau khi nhấn Done
    const dropdown = document.getElementById("list-dropdown");
    dropdown.style.visibility = "hidden";
    dropdown.style.opacity = "0";
}

// Lắng nghe sự kiện 'input' trên thanh tìm kiếm để cập nhật kết quả tìm kiếm ngay lập tức
document.getElementById("search-bar").addEventListener("input", function () {
    applySearch();  // Cập nhật kết quả tìm kiếm ngay lập tức
});

