<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/devicemanager/dv_dss_record/dv_dss_record.js此处编写
 -->
<template>
    <view-grid ref="grid"
               :columns="columns"
               :detail="detail"
               :editFormFields="editFormFields"
               :editFormOptions="editFormOptions"
               :searchFormFields="searchFormFields"
               :searchFormOptions="searchFormOptions"
               :table="table"
               :extend="extend">
    </view-grid>
</template>
<script>
    import extend from "@/extension/devicemanager/dv_dss_record/dv_dss_record.js";
    import { ref, defineComponent } from "vue";
    export default defineComponent({
        setup() {
            const table = ref({
                key: 'record_id',
                footer: "Foots",
                cnName: '点检保养',
                name: 'dv_dss_record/dv_dss_record',
                url: "/dv_dss_record/",
                sortName: "record_id"
            });
            const editFormFields = ref({"record_code":"","record_type":"","record_result":"","cycle_type":"","cycle_count":"","machinery_code":"","machinery_name":"","start_date":"","end_date":"","remark":""});
            const editFormOptions = ref([[{"title":"点检保养编号","field":"record_code","colSize":6,"type":"text"},
                               {"dataKey":"设备点检保养类型","data":[],"title":"点检保养类型","field":"record_type","colSize":6,"type":"select"}],
                              [{"dataKey":"设备点检保养结果","data":[],"title":"点检保养结果","required":true,"field":"record_result","colSize":6,"type":"select"},
                               {"title":"频率","required":true,"field":"cycle_count","colSize":3,"type":"number"},
                               {"dataKey":"设备点检保养周期","data":[],"title":"周期","required":true,"field":"cycle_type","colSize":3,"type":"select"}],
                              [{"title":"设备编码","required":true,"field":"machinery_code","colSize":6,"type":"text"},
                               {"title":"设备名称","required":true,"field":"machinery_name","colSize":6,"type":"text"}],
                              [{"title":"开始时间","required":true,"field":"start_date","colSize":6,"type":"date"},
                               {"title":"结束时间","required":true,"field":"end_date","colSize":6,"type":"date"}],
                              [{"title":"备注","field":"remark","colSize":12,"type":"text"}]]);
            const searchFormFields = ref({"record_code":"","record_type":"","record_result":""});
            const searchFormOptions = ref([[{"title":"点检保养编号","field":"record_code","type":"like"},{"dataKey":"设备点检保养类型","data":[],"title":"点检保养类型","field":"record_type","type":"select"},{"dataKey":"设备点检保养结果","data":[],"title":"点检保养结果","field":"record_result","type":"select"}]]);
            const columns = ref([{field:'record_id',title:'记录ID',type:'bigint',width:110,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'record_code',title:'点检保养编号',type:'string',link:true,width:150,align:'left',sort:true},
                       {field:'record_type',title:'点检保养类型',type:'string',bind:{ key:'设备点检保养类型',data:[]},width:120,align:'left'},
                       {field:'record_result',title:'点检保养结果',type:'string',bind:{ key:'设备点检保养结果',data:[]},width:110,require:true,align:'left'},
                       {field:'machinery_id',title:'设备ID',type:'bigint',width:110,hidden:true,require:true,align:'left'},
                       {field:'machinery_code',title:'设备编码',type:'string',width:120,require:true,align:'left'},
                       {field:'machinery_name',title:'设备名称',type:'string',width:220,require:true,align:'left'},
                       {field:'cycle_type',title:'周期',type:'string',bind:{ key:'设备点检保养周期',data:[]},width:110,require:true,align:'left'},
                       {field:'cycle_count',title:'频率',type:'int',width:110,require:true,align:'left'},
                       {field:'start_date',title:'开始时间',type:'datetime',width:150,require:true,align:'left',sort:true},
                       {field:'end_date',title:'结束时间',type:'datetime',width:150,require:true,align:'left',sort:true},
                       {field:'status',title:'单据状态',type:'string',width:120,hidden:true,align:'left'},
                       {field:'remark',title:'备注',type:'string',width:220,align:'left'},
                       {field:'attr1',title:'预留字段1',type:'string',width:120,hidden:true,align:'left'},
                       {field:'attr2',title:'预留字段2',type:'string',width:220,hidden:true,align:'left'},
                       {field:'attr3',title:'预留字段3',type:'int',width:110,hidden:true,align:'left'},
                       {field:'attr4',title:'预留字段4',type:'int',width:110,hidden:true,align:'left'},
                       {field:'CreateID',title:'创建者ID',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:100,hidden:true,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'datetime',width:150,hidden:true,align:'left',sort:true},
                       {field:'ModifyID',title:'更新者ID',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Modifier',title:'更新者',type:'string',width:100,hidden:true,align:'left'},
                       {field:'ModifyDate',title:'更新时间',type:'datetime',width:150,hidden:true,align:'left',sort:true}]);
            const detail = ref({
                cnName: "#detailCnName",
                table: "#detailTable",
                columns: [],
                sortName: "",
                key: ""
            });
            return {
                table,
                extend,
                editFormFields,
                editFormOptions,
                searchFormFields,
                searchFormOptions,
                columns,
                detail,
            };
        },
    });
</script>
