// Select all checkboxes
const selectAllCheckbox = document.getElementById("select-all");

selectAllCheckbox.addEventListener("change", function () {
    const checkboxes = document.querySelectorAll(".select-user");
    checkboxes.forEach(checkbox => {
        checkbox.checked = this.checked; // Cập nhật trạng thái checkbox user theo "select-all"

        // Thêm/xóa lớp 'selected' khi chọn checkbox
        const row = checkbox.closest('tr');
        if (this.checked) {
            row.classList.add('selected'); // Thêm lớp 'selected' khi checkbox được chọn
        } else {
            row.classList.remove('selected'); // Xóa lớp 'selected' khi checkbox bị bỏ chọn
        }
    });

    // Cập nhật bộ đếm số lượng đã chọn
    document.querySelector(".selected-count").textContent = `${this.checked ? checkboxes.length : 0} Selected`;
});

// Update "Select All" based on individual checkbox changes
document.querySelectorAll(".select-user").forEach(checkbox => {
    checkbox.addEventListener("change", function () {
        const selectedCount = document.querySelectorAll(".select-user:checked").length;
        document.querySelector(".selected-count").textContent = `${selectedCount} Selected`;

        // Cập nhật trạng thái của "Select All" dựa vào tình trạng các checkbox user
        selectAllCheckbox.checked = selectedCount === document.querySelectorAll(".select-user").length;

        // Thêm/xóa lớp 'selected' khi chọn checkbox
        const row = this.closest('tr');
        if (this.checked) {
            row.classList.add('selected');
        } else {
            row.classList.remove('selected');
        }
    });
});
