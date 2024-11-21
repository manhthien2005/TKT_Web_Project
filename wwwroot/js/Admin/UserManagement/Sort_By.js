
function toggleDropdown() {
    const dropdown = document.getElementById("dropdown");
    dropdown.classList.toggle("show");
}


window.onclick = function (event) {
    if (!event.target.closest('.sort-container')) {
        const dropdown = document.getElementById("dropdown");
        dropdown.classList.remove("show");
    }

    if (!event.target.closest('.filter-container')) {
        const dropdown = document.getElementById("filter-dropdown");
        dropdown.classList.remove("show");
    }
}


function applySort() {
    const sortBy = document.getElementById("sort-by").value;
    const sortType = document.getElementById("sort-type").value;

    if (!sortBy || !sortType) {
        alert("Please select both Sort By and Type options.");
        return;
    }

    console.log(`Sorting by: ${sortBy} in ${sortType === 'az' ? 'A → Z' : 'Z → A'} order`);

    const rows = Array.from(document.querySelectorAll(".user-row")); // Lấy tất cả các dòng user
    const tbody = document.querySelector(".user-table tbody");

    rows.sort((a, b) => {
        let valA, valB;

        // Lấy giá trị cần so sánh dựa trên `sortBy`
        switch (sortBy) {
            case "username":
                valA = a.querySelector(".toggle-column").textContent.trim();
                valB = b.querySelector(".toggle-column").textContent.trim();
                break;
            case "name":
                valA = a.querySelectorAll(".toggle-column")[1].textContent.trim();
                valB = b.querySelectorAll(".toggle-column")[1].textContent.trim();
                break;
            case "email":
                valA = a.querySelectorAll(".toggle-column")[3].textContent.trim();
                valB = b.querySelectorAll(".toggle-column")[3].textContent.trim();
                break;
            case "address":
                valA = a.nextElementSibling.querySelector('.detail-column p:nth-child(1)').textContent.split(":")[1]?.trim();
                valB = b.nextElementSibling.querySelector('.detail-column p:nth-child(1)').textContent.split(":")[1]?.trim();
                break;
            case "create-date":
                valA = new Date(a.nextElementSibling.querySelector('.detail-column p:nth-child(2)').textContent.split(":")[1]?.trim());
                valB = new Date(b.nextElementSibling.querySelector('.detail-column p:nth-child(2)').textContent.split(":")[1]?.trim());
                break;
        }

        // So sánh giá trị
        if (sortType === "az") {
            return valA > valB ? 1 : valA < valB ? -1 : 0;
        } else {
            return valA < valB ? 1 : valA > valB ? -1 : 0;
        }
    });

    // Gắn lại các dòng đã được sắp xếp vào bảng
    rows.forEach(row => {
        const detailsRow = row.nextElementSibling; // Dòng chi tiết đi kèm
        tbody.appendChild(row); // Thêm lại dòng đã sắp xếp vào tbody
        tbody.appendChild(detailsRow); // Thêm lại dòng chi tiết vào tbody
    });

    // Ẩn dropdown sau khi sắp xếp
    const dropdown = document.getElementById("dropdown");
    dropdown.classList.remove("show");
}
