<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/devicemanager/dv_repair/dv_repair.js此处编写
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
    import extend from "@/extension/devicemanager/dv_repair/dv_repair.js";
    import { ref, defineComponent } from "vue";
    export default defineComponent({
        setup() {
            const table = ref({
                key: 'repair_id',
                footer: "Foots",
                cnName: '设备维修',
                name: 'dv_repair/dv_repair',
                url: "/dv_repair/",
                sortName: "repair_id"
            });
            const editFormFields = ref({"repair_code":"","repair_name":"","machinery_code":"","machinery_name":"","require_date":"","finish_date":"","confirm_date":"","repair_result":"","accepted_by":"","confirm_by":"","remark":""});
            const editFormOptions = ref([[{"title":"维修单编号","field":"repair_code","type":"text"},
                               {"title":"维修单名称","required":true,"field":"repair_name","type":"text"}],
                              [{"title":"设备编码","required":true,"field":"machinery_code","disabled":true,"type":"text"},
                               {"title":"设备名称","required":true,"field":"machinery_name","disabled":true,"type":"text"}],
                              [{"title":"报修日期","field":"require_date","type":"datetime"},
                               {"title":"维修完成日期","field":"finish_date","type":"datetime"}],
                              [{"title":"验收日期","field":"confirm_date","type":"datetime"},
                               {"dataKey":"维修结果","data":[],"title":"维修结果","field":"repair_result","type":"select"}],
                              [{"title":"维修人员","field":"accepted_by","type":"text"},
                               {"title":"验收人员","field":"confirm_by","type":"text"}],
                              [{"title":"备注","field":"remark","colSize":12,"type":"text"}]]);
            const searchFormFields = ref({"repair_code":"","repair_name":"","machinery_code":"","machinery_name":"","repair_result":"","accepted_by":""});
            const searchFormOptions = ref([[{"title":"维修单编号","field":"repair_code","type":"like"},{"title":"维修单名称","field":"repair_name","type":"text"},{"title":"设备编码","field":"machinery_code","type":"text"},{"title":"设备名称","field":"machinery_name","type":"text"}],[{"dataKey":"维修结果","data":[],"title":"维修结果","field":"repair_result","type":"select"},{"title":"维修人员","field":"accepted_by","type":"text"}]]);
            const columns = ref([{field:'repair_id',title:'维修单ID',type:'bigint',width:110,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'repair_code',title:'维修单编号',type:'string',link:true,width:120,align:'left',sort:true},
                       {field:'repair_name',title:'维修单名称',type:'string',width:220,require:true,align:'left'},
                       {field:'machinery_id',title:'设备ID',type:'bigint',width:110,hidden:true,require:true,align:'left'},
                       {field:'machinery_code',title:'设备编码',type:'string',width:120,readonly:true,require:true,align:'left'},
                       {field:'machinery_name',title:'设备名称',type:'string',width:220,readonly:true,require:true,align:'left'},
                       {field:'machinery_type_id',title:'设备类型ID',type:'bigint',width:110,hidden:true,require:true,align:'left'},
                       {field:'require_date',title:'报修日期',type:'datetime',width:150,align:'left',sort:true},
                       {field:'finish_date',title:'维修完成日期',type:'datetime',width:150,align:'left',sort:true},
                       {field:'confirm_date',title:'验收日期',type:'datetime',width:150,align:'left',sort:true},
                       {field:'repair_result',title:'维修结果',type:'string',bind:{ key:'维修结果',data:[]},width:120,align:'left'},
                       {field:'accepted_by',title:'维修人员',type:'string',width:120,align:'left'},
                       {field:'confirm_by',title:'验收人员',type:'string',width:120,align:'left'},
                       {field:'status',title:'单据状态',type:'string',width:120,hidden:true,align:'left'},
                       {field:'remark',title:'备注',type:'string',width:220,align:'left'},
                       {field:'attr1',title:'预留字段1',type:'string',width:120,hidden:true,align:'left'},
                       {field:'attr2',title:'预留字段2',type:'string',width:220,hidden:true,align:'left'},
                       {field:'attr3',title:'预留字段3',type:'int',width:110,hidden:true,align:'left'},
                       {field:'attr4',title:'预留字段4',type:'int',width:110,hidden:true,align:'left'},
                       {field:'CreateID',title:'创建者ID',type:'int',width:120,hidden:true,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:120,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'datetime',width:120,align:'left',sort:true},
                       {field:'ModifyID',title:'更新者ID',type:'int',width:120,hidden:true,align:'left'},
                       {field:'Modifier',title:'更新者',type:'string',width:120,align:'left'},
                       {field:'ModifyDate',title:'更新时间',type:'datetime',width:120,align:'left',sort:true}]);
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
