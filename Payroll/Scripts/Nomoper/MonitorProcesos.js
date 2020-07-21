                                                                $(function () {

    /// Funcion de progres bar //
    var EnCola = 0;
    var Procesando = 0;
    var terminado = 0;
    var PorMultiplicador;
    var TotalRows=0;

    FprogresBar = () => {

        //// pone los valores a los estados de procesos de trabajos/// 

        $.ajax({
            url: "../Nomina/ListStatusProcesosJobs",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                if (data.length > 0) {
                  var  rosw = data.length;
                }
                for (i = 0; i < rosw; i++) {

                    if (data[i].sEstatusJobs == "Terminado") {
                       terminado = data[i].iIdTarea;
                    }

                    if (data[i].sEstatusJobs == "Procesando") {
                       Procesando = data[i].iIdTarea;
                    }
                    if (data[i].sEstatusJobs == "En Cola") {
                        EnCola = data[i].iIdTarea;
                    }
                }
                 
                TotalRows = EnCola + terminado + Procesando;
                console.log(TotalRows);
                PorMultiplicador = terminado * 100;              
                PorMultiplicador = (PorMultiplicador / TotalRows); 
             
                var renderText = function (text, value) {
                    if (value < 55) {
                        return "<span style='color: #333;'>" + text + "</span>";
                    }
                    return "<span style='color: #fff; '>" + text + "</span>";
                };
           
                $("#jqxProgressBar2").jqxProgressBar({ animationDuration: 0, showText: true, renderText: renderText, template: "primary", width: 980, height: 20, value: PorMultiplicador });
                var values = {};
                var addInterval = function (id, intervalStep) {                                                        
                    values[id] = { value: PorMultiplicador };
                    values[id].interval = setInterval(function () {
                        $.ajax({
                            url: "../Nomina/ListStatusProcesosJobs",
                            type: "POST",
                            data: JSON.stringify(),
                            contentType: "application/json; charset=utf-8",
                            success: (data) => {
                                console.log(data.length);
                                for (i = 0; i < rosw - 1; i++) {
                                    if (data[i].sEstatusJobs != ' ' || data[i].sEstatusJobs != '' || data[i].sEstatusJobs != null) {
                                        if (data[i].sEstatusJobs == "Terminado") {
                                            terminado = data[i].iIdTarea;
                                            PorMultiplicador = terminado * 100;
                                            PorMultiplicador = (PorMultiplicador / TotalRows);
                                            values[id].value = PorMultiplicador;
                                            DgridTBProcesos();
                                            $("#" + id).val(values[id].value);
                                        }

                                        if (data[i].sEstatusJobs == "Procesando") {
                                            Procesando = data[i].iIdTarea;
                                        }
                                        if (data[i].sEstatusJobs == "En Cola") {
                                            EnCola = data[i].iIdTarea;
                                        }
                                    }
                                }

                            },
                        })     
                            if (values[id].value >= 100) {
                            DgridTBProcesos();
                            clearInterval(values[id].interval);
                            
                        }
                    }, intervalStep);
                }
                addInterval("jqxProgressBar2",20000);
            },

        });
     
    }

    FprogresBar();

    DgridTBProcesos = () => {       
        $.ajax({
            url: "../Nomina/ListTBProcesosJobs",
            type: "POST",
            data: JSON.stringify(),
            success: (data) => {
                console.log('info de Base');
                console.log(data);
                var source =
                {
                    localdata: data,
                    datatype: "array",
                    datafields:
                        [
                            { name: 'iIdTarea', type: 'string' },
                            { name: 'iIdJobs', type: 'string' },
                            { name: 'sEstatusJobs', type: 'string' },
                            { name: 'sNombre', type: 'string' },
                            { name: 'sParametros', type: 'string'},
                        ]
                };

                var dataAdapter = new $.jqx.dataAdapter(source);

                $("#TbProcesos").jqxGrid(
                    {
                        width: 980,
                        source: dataAdapter,
                        columnsresize: true,
                        columns: [
                            { text: 'No Tarea', datafield: 'iIdTarea', width: 100 },
                            { text: 'Id Proceso', datafield: 'iIdJobs', width: 200 },
                            { text: 'Estatus de procesos', datafield: 'sEstatusJobs', whidth: 200 },
                            { text: 'Nombre de Proceso', datafield: 'sNombre', whidt: 100 },
                            { text: 'Parametros', datafield: 'sParametros', whidt: 100 },                          
                        ]
                   });
            }
        });  
    }
    DgridTBProcesos();

});