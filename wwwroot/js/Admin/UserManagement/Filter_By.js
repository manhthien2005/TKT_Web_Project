// Hàm để toggle (hiển thị hoặc ẩn) dropdown Filter
function toggleFilterDropdown() {
    const dropdown = document.getElementById("filter-dropdown");
    dropdown.classList.toggle("show");
}

// Hàm xử lý lựa chọn "Filter By"
function updateFilterOptions() {
    const filterBy = document.getElementById("filter-by").value;

    if (filterBy === "gender") {
        document.getElementById("gender-options").style.display = "block";
        document.getElementById("status-options").style.display = "none";
    } else if (filterBy === "status") {
        document.getElementById("gender-options").style.display = "none";
        document.getElementById("status-options").style.display = "block";
    }
}

function applyFilter() {
    const filterBy = document.getElementById("filter-by").value;
    let selectedValue = "";

    if (filterBy === "gender") {
        selectedValue = document.getElementById("gender-select").value;
    } else if (filterBy === "status") {
        selectedValue = document.getElementById("status-select").value;
    }

    // Lấy tất cả các hàng người dùng
    const rows = document.querySelectorAll('.user-row');

    rows.forEach(row => {
        const detailsRow = row.nextElementSibling; // Dòng chi tiết
        // Lấy thông tin giới tính từ Gender
        const genderText = Array.from(detailsRow.querySelectorAll('.detail-column p'))
            .find(p => p.textContent.includes("Gender:"))?.textContent || "";

        const gender = genderText.split("Gender:")[1]?.trim(); // Không cần toLowerCase vì so sánh trực tiếp
        const status = row.querySelector('.status')?.textContent.toLowerCase() || "";

        // Hiển thị hoặc ẩn dòng dựa trên bộ lọc
        if ((filterBy === "gender" && gender.includes(selectedValue)) ||
            (filterBy === "status" && status.includes(selectedValue)) ||
            filterBy === "") {
            row.style.display = "";
            detailsRow.style.display = "none"; // Ẩn chi tiết theo mặc định
        } else {
            row.style.display = "none";
            detailsRow.style.display = "none"; // Ẩn luôn chi tiết nếu không khớp
        }
    });

    // Ẩn dropdown sau khi áp dụng
    const dropdown = document.getElementById("filter-dropdown");
    dropdown.classList.remove("show");

    // Cập nhật placeholder tìm kiếm
    const searchInput = document.querySelector(".search-bar");
    searchInput.placeholder = `Filter - ${selectedValue.charAt(0).toUpperCase() + selectedValue.slice(1)} -`;
}



// Cập nhật mặc định khi trang được tải và khi thay đổi lựa chọn filter
window.onload = function () {
    updateFilterOptions();  // Mặc định là chọn "gender"
    const filterSelect = document.getElementById("filter-by");
    filterSelect.addEventListener("change", updateFilterOptions);  // Cập nhật khi người dùng thay đổi filter
};
