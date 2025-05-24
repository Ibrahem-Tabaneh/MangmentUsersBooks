$(document).ready(function () {
    // تهيئة جدول الأدوار
    $('#tableRole').DataTable({
        "autoWidth": false,
        "responsive": true
    });
});

// وظيفة لحذف عنصر
function Delete(id) {
    Swal.fire({
        title: lbSure,
        text: lbCantBack,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: lbConfirmDelete
    }).then((result) => {
        if (result.isConfirmed) {
            // إعادة التوجيه إلى عملية الحذف
            window.location.href = `/Admin/Sliders/Delete?Id=${id}`;
        }
    });
}

// وظيفة لتحرير البيانات
Edit = (id, name, desc, img) => {
    document.getElementById("tittlee").innerHTML = lbEditPage;
    document.getElementById("btnSave").value = lbbtnEdit;

    // تعيين القيم في الحقول
    document.getElementById("sliderId").value = id;
    document.getElementById("sliderName").value = name;
    document.getElementById("pageDesc").value = desc;

    // عرض صورة السلايدر
    document.getElementById("img").hidden = false;
    document.getElementById("img").src = '/Images/Page/' + img;
}
