$(document).ready(function () {
    // تهيئة جداول DataTable
    $('#tableCaregory').DataTable({
        "autoWidth": false,
        "responsive": true
    });

    $('#tableCaregoryLog').DataTable({
        "autoWidth": false,
        "responsive": true
    });

    // فتح التبويب الافتراضي
    document.getElementById("defaultOpen").click();
});

// وظيفة لحذف فئة
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
            window.location.href = `/Admin/Categories/Delete?Id=${id}`;
        }
    });
}

// وظيفة لتحرير فئة
Edit = (id, name, desc) => {
    document.getElementById("tittlee").innerHTML = tittleEditCategory;
    document.getElementById("btnSave").value = lbBtnEdit;
    document.getElementById("categoryId").value = id;
    document.getElementById("categoryName").value = name;
    document.getElementById("categoryDesc").value = desc;
}

// وظيفة لإعادة تعيين الحقول
Reset = () => {
    document.getElementById("tittlee").innerHTML = lbBtnSaveNewCategory;
    document.getElementById("btnSave").value = lbbtnSave;
    document.getElementById("categoryName").value = "";
    document.getElementById("categoryDesc").value = "";
    document.getElementById("categoryId").value = "00000000-0000-0000-0000-000000000000"; // Guid.Empty
}

// وظيفة لحذف سجل
function DeleteLog(id) {
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
            window.location.href = `/Admin/Categories/DeleteLog?Id=${id}`;
        }
    });
}

// وظيفة لإدارة علامات التبويب
function openCity(evt, cityName) {
    var i, tabcontent, tablinks;

    // إخفاء جميع محتويات التبويبات
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // إزالة الصنف "active" من جميع الأزرار
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // عرض التبويب الحالي وإضافة الصنف "active" إلى الزر الذي فتح التبويب
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}
