const downloadCustomersBtn = document.getElementById("customers_download_btn");
const numOfCustomers = document.getElementById("download_customers_num");

numOfCustomers.addEventListener("change", (e) => {
    downloadCustomersBtn.setAttribute("href", "/download?limit=" + e.target.value);
})