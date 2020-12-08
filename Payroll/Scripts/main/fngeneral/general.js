$(function () {

    const fCatchError = (error) => {
        let msj = "";
        if (error instanceof EvalError) {
            msj = `EvalError: ${error.message}`;
        } else if (error instanceof TypeError) {
            msj = `TypeError: ${error.message}`;
        } else if (error instanceof RangeError) {
            msj = `RangeError: ${error.message}`;
        } else {
            msj = `Error: ${error}`;
        }
        return msj;
    }

    module.exports = fCatchError

});