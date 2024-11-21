document.getElementById("user-form").addEventListener("submit", async function (event) {
    event.preventDefault();

    const formData = new FormData(this);

    try {
        const response = await fetch("/Admin/AddUser", {
            method: "POST",
            body: formData
        });

        const result = await response.text();
        document.getElementById("success-messages").innerText = result;
    } catch (error) {
        console.error("Error adding user:", error);
    }
});