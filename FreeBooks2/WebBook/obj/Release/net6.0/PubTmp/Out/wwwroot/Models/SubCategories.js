$(document).ready(function () {
    // تهيئة جدول الفئات
    $('#tableCaregory').DataTable({
        "autoWidth": false,
        "responsive": true
    });

    // تهيئة جدول سجل الفئات
    $('#tableCaregoryLog').DataTable({
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
            window.location.href = `/Admin/SubCategories/Delete?Id=${id}`;
        }
    });
}

// وظيفة لتحرير عنصر
Edit = (id, name, categoryId) => {
    document.getElementById("tittlee").innerHTML = tittleEditSubCategory;
    document.getElementById("btnSave").value = lbBtnEdit;
    document.getElementById("categoryId").value = id;
    document.getElementById("categoryName").value = name;
    document.getElementById("CategoriesList").value = categoryId;
}

// وظيفة لإعادة تعيين النموذج
Reset = () => {
    document.getElementById("tittlee").innerHTML = lbBtnSaveNewSubCategory;
    document.getElementById("btnSave").value = lbbtnSave;
    document.getElementById("categoryName").value = "";
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
        confirmButtonText: lbConfirmDelete,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Admin/SubCategories/DeleteLog?Id=${id}`;
        }
    });
}

// تهيئة التبويبات
document.getElementById("defaultOpen").click();

// وظيفة لفتح تبويب
function openCity(evt, cityName) {
    // إخفاء جميع محتويات التبويبات
    var tabcontent = document.getElementsByClassName("tabcontent");
    for (var i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // إزالة الفئة النشطة من جميع الروابط
    var tablinks = document.getElementsByClassName("tablinks");
    for (var i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // عرض التبويب الحالي وإضافة فئة "نشطة" للرابط الذي فتح التبويب
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}
