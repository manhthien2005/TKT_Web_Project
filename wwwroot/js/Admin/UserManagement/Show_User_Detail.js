function toggleDetails(detailsRow) {
    // Chỉ thay đổi display của dòng chi tiết, không phải dòng dữ liệu chính
    detailsRow.style.display = (detailsRow.style.display === "none" || detailsRow.style.display === "")
        ? "table-row"
        : "none";
}

document.querySelectorAll(".user-row .toggle-column").forEach(column => {
    column.addEventListener("click", function () {
        // Tìm dòng chi tiết ngay dưới dòng người dùng
        const detailsRow = this.closest("tr").nextElementSibling;

        // Kiểm tra nếu dòng chi tiết có tồn tại và có class "user-details-row"
        if (detailsRow && detailsRow.classList.contains("user-details-row")) {
            toggleDetails(detailsRow);
        }
    });
});
