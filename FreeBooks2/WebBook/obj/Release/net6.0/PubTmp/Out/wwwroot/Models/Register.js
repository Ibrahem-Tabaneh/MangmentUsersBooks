$(document).ready(function () {
    // تهيئة جدول الأدوار
    $('#tableRole').DataTable({
        "autoWidth": false,
        "responsive": true
    });
});

// وظيفة لحذف المستخدم
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
            // إعادة التوجيه إلى عملية الحذف
            window.location.href = `/Admin/Accounts/DeleteUser?Id=${id}`;
        }
    });
}

// وظيفة لتحرير بيانات المستخدم
Edit = (id, name, email, active, img, Role) => {
    document.getElementById("tittlee").innerHTML = lbEditDataUser;
    document.getElementById("btnSave").value = lbbtnEdit;

    // ضبط القيم في الحقول
    document.getElementById("userId").value = id;
    document.getElementById("userName").value = name;
    document.getElementById("userEmail").value = email;
    document.getElementById("RolesList").value = Role;

    // عرض صورة المستخدم
    document.getElementById("img").hidden = false;
    document.getElementById("img").src = '/Images/User/' + img;

    // إخفاء حقول كلمة المرور
    $('#group_password').hide();
    $('#group_compare').hide();

    // ضبط حالة الـ checkbox حسب حالة المستخدم
    var activeUser = document.getElementById("ActiveUser");
    activeUser.checked = (active === "True");

    // تعيين قيمة افتراضية لكلمات المرور
    document.getElementById("userPassword").value = "$$Ww12345";
    document.getElementById("userComparePassword").value = "$$Ww12345";
}

// وظيفة لإعادة تعيين النموذج
Reset = () => {
    document.getElementById("tittlee").innerHTML = lbCreateUser;
    document.getElementById("btnSave").value = lbbtnSave;

    // إعادة تعيين القيم
    document.getElementById("userId").value = "";
    document.getElementById("userName").value = "";
    document.getElementById("userEmail").value = "";
    document.getElementById("RolesList").value = "";

    // إعادة عرض حقول كلمة المرور
    $('#group_password').show();
    $('#group_compare').show();

    // ضبط حالة الـ checkbox لتكون غير مفعل
    document.getElementById("ActiveUser").checked = false;

    // إعادة تعيين قيم كلمات المرور
    document.getElementById("userPassword").value = "";
    document.getElementById("userComparePassword").value = "";

    // إخفاء الصورة
    document.getElementById("img").hidden = true;
    document.getElementById("img").src = "";

    // تعيين قيمة Guid.Empty
    document.getElementById("userId").value = "00000000-0000-0000-0000-000000000000"; // Guid.Empty
}

// وظيفة لتغيير كلمة المرور
ChangePassword = (id) => {
    document.getElementById("userIdPassword").value = id;
}
