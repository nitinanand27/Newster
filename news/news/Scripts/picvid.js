
document.getElementById("dis_img").onblur = function () {
    if (this.value.length > 0) {
        document.getElementById("dis_vid").disabled = true;       
    } else {
        document.getElementById("dis_vid").disabled = false;
    }
}

document.getElementById("dis_vid").onblur = function () {
    if (this.value.length > 0) {
        document.getElementById("dis_img").disabled = true;
    } else {
        document.getElementById("dis_img").disabled = false;

    }

}