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
        confirmButtonText: lbConfirmDelete,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Admin/Sliders/Delete?Id=${id}`;
        }
    });
}

// وظيفة لتحرير شريحة
Edit = (id, name, img) => {
    document.getElementById("tittlee").innerHTML = lbEditSlider;
    document.getElementById("btnSave").value = lbbtnEdit;

    document.getElementById("sliderId").value = id;
    document.getElementById("sliderName").value = name;
    document.getElementById("img").hidden = false;
    document.getElementById("img").src = '/Images/Slider/' + img;
}

// وظيفة لإعادة تعيين النموذج
Reset = () => {
    document.getElementById("tittlee").innerHTML = lbCreateSlider;
    document.getElementById("btnSave").value = lbbtnSave;

    document.getElementById("sliderName").value = "";
    document.getElementById("img").hidden = true;
    document.getElementById("img").src = "";
    document.getElementById("sliderId").value = "00000000-0000-0000-0000-000000000000"; // Guid.Empty
}
