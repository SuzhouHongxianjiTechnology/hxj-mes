<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/devicemanager/tm_tool/tm_tool.js此处编写
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
    import extend from "@/extension/devicemanager/tm_tool/tm_tool.js";
    import { ref, defineComponent } from "vue";
    export default defineComponent({
        setup() {
            const table = ref({
                key: 'tool_id',
                footer: "Foots",
                cnName: '易损件管理',
                name: 'tm_tool/tm_tool',
                url: "/tm_tool/",
                sortName: "tool_id"
            });
            const editFormFields = ref({"tool_code":"","tool_name":"","tool_type_id":"","brand":"","mainten_type":"","quantity":"","quantity_avail":"","quantity_avail_remain":"","next_mainten_period":"","next_mainten_date":"","status":"","remark":""});
            const editFormOptions = ref([[{"title":"易损件编码","field":"tool_code","type":"text"},
                               {"title":"易损件名称","required":true,"field":"tool_name","type":"text"}],
                              [{"dataKey":"易损件类型下拉","data":[],"title":"易损件类型","field":"tool_type_id","type":"select"},
                               {"title":"品牌","field":"brand","type":"text"}],
                              [{"dataKey":"易损件保养类型","data":[],"title":"保养维护类型","field":"mainten_type","type":"select"},
                               {"title":"总次数","required":true,"field":"quantity","type":"number"}],
                              [{"title":"已用次数","field":"quantity_avail","disabled":true,"type":"number"},
                               {"title":"剩余次数","field":"quantity_avail_remain","disabled":true,"type":"number"}],
                              [{"title":"下一次保养周期","field":"next_mainten_period","type":"datetime"},
                               {"title":"下一次保养日期","field":"next_mainten_date","type":"datetime"}],
                              [{"title":"备注","field":"remark","type":"text"},
                               {"dataKey":"单据状态","data":[],"title":"状态","field":"status","type":"select"}]]);
            const searchFormFields = ref({"tool_code":"","tool_name":"","tool_type_id":"","mainten_type":""});
            const searchFormOptions = ref([[{"title":"易损件编码","field":"tool_code","type":"like"},{"title":"易损件名称","field":"tool_name","type":"text"},{"dataKey":"易损件类型下拉","data":[],"title":"易损件类型","field":"tool_type_id","type":"select"},{"dataKey":"易损件保养类型","data":[],"title":"保养维护类型","field":"mainten_type","type":"select"}]]);
            const columns = ref([{field:'tool_id',title:'易损件ID',type:'bigint',width:110,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'tool_code',title:'易损件编码',type:'string',link:true,width:150,align:'left',sort:true},
                       {field:'tool_name',title:'易损件名称',type:'string',width:220,require:true,align:'left'},
                       {field:'tool_type_id',title:'易损件类型',type:'bigint',bind:{ key:'易损件类型下拉',data:[]},width:120,align:'left'},
                       {field:'brand',title:'品牌',type:'string',width:220,align:'left'},
                       {field:'spec',title:'型号',type:'string',width:220,hidden:true,align:'left'},
                       {field:'mainten_type',title:'保养维护类型',type:'string',bind:{ key:'易损件保养类型',data:[]},width:110,align:'left'},
                       {field:'quantity',title:'总次数',type:'int',width:110,require:true,align:'left'},
                       {field:'quantity_avail',title:'已用次数',type:'int',width:110,readonly:true,align:'left'},
                       {field:'quantity_avail_remain',title:'剩余次数',type:'int',width:110,readonly:true,align:'left'},
                       {field:'next_mainten_period',title:'下一次保养周期',type:'int',width:110,align:'left'},
                       {field:'next_mainten_date',title:'下一次保养日期',type:'datetime',width:150,align:'left',sort:true},
                       {field:'status',title:'状态',type:'string',bind:{ key:'单据状态',data:[]},width:120,hidden:true,align:'left'},
                       {field:'remark',title:'备注',type:'string',width:220,align:'left'},
                       {field:'attr1',title:'预留字段1',type:'string',width:120,hidden:true,align:'left'},
                       {field:'attr2',title:'预留字段2',type:'string',width:220,hidden:true,align:'left'},
                       {field:'attr3',title:'预留字段3',type:'int',width:110,hidden:true,align:'left'},
                       {field:'attr4',title:'预留字段4',type:'int',width:110,hidden:true,align:'left'},
                       {field:'CreateID',title:'创建者ID',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:100,hidden:true,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'datetime',width:150,hidden:true,align:'left',sort:true},
                       {field:'ModifyID',title:'创建者ID',type:'int',width:100,hidden:true,align:'left'},
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
