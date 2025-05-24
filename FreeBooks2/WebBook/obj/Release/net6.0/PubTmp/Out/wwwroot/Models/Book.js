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

// وظيفة لحذف كتاب
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
            window.location.href = `/Admin/Books/Delete?Id=${id}`;
        }
    });
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
            window.location.href = `/Admin/Books/DeleteLog?Id=${id}`;
        }
    });
}

// وظيفة لتحرير بيانات الكتاب
Edit = (id, name, categoryId, img, publish, subCategoryId, Author, Desc, imgAuthor) => {
    document.getElementById("tittlee").innerHTML = tittleEditBook;
    document.getElementById("btnSave").value = lbBtnEdit;

    document.getElementById("bookId").value = id;
    document.getElementById("bookName").value = name;
    document.getElementById("bookDesc").value = Desc;
    document.getElementById("authorName").value = Author;

    // إعداد الصور
    document.getElementById("img").hidden = false;
    document.getElementById("img").src = '/Images/Book/' + img;

    document.getElementById("imgAuthor").hidden = false;
    document.getElementById("imgAuthor").src = '/Images/Author/' + imgAuthor;

    // ضبط حالة الـ checkbox حسب حالة النشر
    var PublishBook = document.getElementById("Publish");
    PublishBook.checked = publish === "True";

    document.getElementById("CategoriesList").value = categoryId;
    document.getElementById("SubCategoriesList").value = subCategoryId;
}

// وظيفة لإعادة تعيين الحقول
Reset = () => {
    document.getElementById("tittlee").innerHTML = lbBtnSaveNewBook;
    document.getElementById("btnSave").value = lbbtnSave;

    document.getElementById("bookName").value = "";
    document.getElementById("bookDesc").value = "";
    document.getElementById("authorName").value = "";
    document.getElementById("Publish").checked = false;

    document.getElementById("CategoriesList").value = "";
    document.getElementById("SubCategoriesList").value = "";

    document.getElementById("img").hidden = true;
    document.getElementById("img").src = "";

    document.getElementById("imgAuthor").hidden = true;
    document.getElementById("imgAuthor").src = "";

    document.getElementById("bookId").value = "00000000-0000-0000-0000-000000000000"; // Guid.Empty
}

// وظيفة لإدارة علامات التبويب
function openCity(evt, cityName) {
    // إخفاء جميع محتويات التبويبات
    var tabcontent = document.getElementsByClassName("tabcontent");
    for (var i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // إزالة الصنف "active" من جميع الأزرار
    var tablinks = document.getElementsByClassName("tablinks");
    for (var i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // عرض التبويب الحالي وإضافة الصنف "active" إلى الزر الذي فتح التبويب
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}
