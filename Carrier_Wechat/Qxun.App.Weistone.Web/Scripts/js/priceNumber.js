var priceNumber = (function () {
    var price = {};
    price.isInputedNum = function (txtId) {
        if (!event.shiftKey) {
            if (!((event.keyCode >= 48 && event.keyCode <= 57)
                || event.keyCode == 8
                || event.keyCode == 9
                || event.keyCode == 46
                || (event.keyCode >= 96 && event.keyCode <= 105)
                || (event.keyCode >= 37 && event.keyCode <= 40))) {
                //alert(event.keyCode);
                event.returnValue = false;
                return false;
            }
            if (event.keyCode != 9) {
                if (txtId.value == "0") {
                    txtId.value = "";
                }
            }
            return true;
        } else {
            event.returnValue = false;
            return false;
        }
    };

    /**
    * remove comar from a number String 
    */
    price.removeComarValue = function (txtId) {
        if (txtId.readOnly == true)
            return;
        var txt = removeComar(txtId.value);
        txtId.value = txt;
    };

    /**
    * remove comar from a number String 
    */
    price.removeComar = removeComar;

    function removeComar(txtNum) {
        var numStr = txtNum + "";
        var indx = numStr.indexOf(",");
        if (indx != -1) {
            numStr = numStr.replace(",", "");
            numStr = removeComar(numStr);
        }
        return numStr;
    };

    /**
    * add comar to a number String 
    */
    price.resetComarValue = function (txtId) {
        if (txtId.readOnly == true) return;
        var value = txtId.value.replace(/^0+/g, '');
        if (value == '') value = '0';
        var txt = resetComar(value);
        txtId.value = txt;
    };

    /**
    * add comar to a number String 
    */
    price.resetComar = resetComar;

    function resetComar(txtNum) {
        txtNum = removeComar(txtNum);
        if (txtNum.length <= 3) {
            return txtNum;
        }
        var threeOnes = new Array(0);
        var last = txtNum.length;
        var first = last - 3;
        var arrayIndx = 0;
        while (first >= 0) {
            threeOnes[arrayIndx] = txtNum.substring(first, last);
            arrayIndx++;
            last = first;
            first = last - 3;
            if (first < 0 && last > 0) {
                first = 0;
            }
        }
        var txtNumCopy = "";
        for (var i = threeOnes.length - 1; i >= 0; i--) {
            if (txtNumCopy == "") {
                txtNumCopy = threeOnes[i];
            } else {
                txtNumCopy = txtNumCopy + "," + threeOnes[i];
            }
        }

        if (txtNumCopy.charAt(0) == '-') {
            if (txtNumCopy.charAt(1) == ',') {
                txtNumCopy = "-" + txtNumCopy.substring(2);
            }
        }

        return txtNumCopy;
    };
    return price;
} ());