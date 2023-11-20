<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/devicemanager/dv_machinery/dv_machinery.js此处编写
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
    import extend from "@/extension/devicemanager/dv_machinery/dv_machinery.js";
    import { ref, defineComponent } from "vue";
    export default defineComponent({
        setup() {
            const table = ref({
                key: 'machinery_id',
                footer: "Foots",
                cnName: '设备台账',
                name: 'dv_machinery/dv_machinery',
                url: "/dv_machinery/",
                sortName: "machinery_id"
            });
            const editFormFields = ref({});
            const editFormOptions = ref([]);
            const searchFormFields = ref({});
            const searchFormOptions = ref([]);
            const columns = ref([{field:'machinery_id',title:'设备类型ID',type:'bigint',width:110,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'machinery_code',title:'设备类型编码',type:'string',width:120,require:true,align:'left',sort:true},
                       {field:'machinery_name',title:'设备类型名称',type:'string',width:220,require:true,align:'left'},
                       {field:'machinery_brand',title:'品牌',type:'string',width:220,align:'left'},
                       {field:'machinery_spec',title:'规格型号',type:'string',width:220,align:'left'},
                       {field:'machinery_type_id',title:'设备类型ID',type:'bigint',width:110,hidden:true,require:true,align:'left'},
                       {field:'machinery_type_code',title:'设备类型编码',type:'string',width:120,hidden:true,align:'left'},
                       {field:'machinery_type_name',title:'设备类型名称',type:'string',width:220,hidden:true,align:'left'},
                       {field:'machinery_ip',title:'设备通讯接口',type:'string',width:120,require:true,align:'left'},
                       {field:'machinery_config',title:'设备配置信息',type:'string',width:110,align:'left'},
                       {field:'workshop_id',title:'所属车间ID',type:'bigint',width:110,hidden:true,require:true,align:'left'},
                       {field:'workshop_code',title:'所属车间编码',type:'string',width:120,hidden:true,align:'left'},
                       {field:'workshop_name',title:'所属车间名称',type:'string',width:220,align:'left'},
                       {field:'status',title:'设备状态',type:'string',width:120,require:true,align:'left'},
                       {field:'remark',title:'备注',type:'string',width:220,align:'left'},
                       {field:'attr1',title:'预留字段1',type:'string',width:120,hidden:true,align:'left'},
                       {field:'attr2',title:'预留字段2',type:'string',width:220,hidden:true,align:'left'},
                       {field:'attr3',title:'预留字段3',type:'int',width:110,hidden:true,align:'left'},
                       {field:'create_by',title:'创建者',type:'string',width:120,hidden:true,align:'left'},
                       {field:'create_time',title:'创建时间',type:'datetime',width:150,hidden:true,align:'left',sort:true},
                       {field:'update_by',title:'更新者',type:'string',width:120,hidden:true,align:'left'},
                       {field:'update_time',title:'更新时间',type:'datetime',width:150,hidden:true,align:'left',sort:true}]);
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
