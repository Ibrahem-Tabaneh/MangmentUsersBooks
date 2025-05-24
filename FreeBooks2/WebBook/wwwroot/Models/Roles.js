$(document).ready(function () {
    // تهيئة جدول الأدوار
    $('#tableRole').DataTable({
        "autoWidth": false,
        "responsive": true
    });
});

// وظيفة لحذف الدور
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
            window.location.href = `/Admin/Accounts/DeleteRole?Id=${id}`;
        }
    });
}

// وظيفة لتحرير الدور
Edit = (id, name) => {
    document.getElementById("tittlee").innerHTML = tittleEditRole;
    document.getElementById("btnSave").value = lbBtnEdit;
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;
}

// وظيفة لإعادة تعيين النموذج
Reset = () => {
    document.getElementById("tittlee").innerHTML = lbbtnSaveNewRole;
    document.getElementById("btnSave").value = lbbtnSave;
    document.getElementById("roleName").value = "";
    document.getElementById("roleId").value = "00000000-0000-0000-0000-000000000000"; // Guid.Empty
}
