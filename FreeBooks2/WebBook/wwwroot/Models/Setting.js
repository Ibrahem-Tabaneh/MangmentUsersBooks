$(document).ready(function () {
    // تهيئة جدول الأدوار
    $('#tableRole').DataTable({
        "autoWidth": false,
        "responsive": true
    });
});

// وظيفة لتحرير الإعدادات
Edit = (id, name, desc, face, instgram, twitter, linkedin, loc, email, contact, img) => {
    document.getElementById("tittlee").innerHTML = lbEditSlider;
    document.getElementById("btnSave").value = lbbtnEdit;

    // تعيين القيم في الحقول المناسبة
    document.getElementById("settingerId").value = id;
    document.getElementById("websiteName").value = name;
    document.getElementById("websiteDesc").value = desc;
    document.getElementById("email").value = email;
    document.getElementById("Facebook").value = face;
    document.getElementById("Instagram").value = instgram;
    document.getElementById("Twitter").value = twitter;
    document.getElementById("Linkedin").value = linkedin;
    document.getElementById("Location").value = loc;
    document.getElementById("Contact").value = contact;

    // عرض الصورة المحدثة
    document.getElementById("img").hidden = false;
    document.getElementById("img").src = '/Images/Setting/' + img;
}
