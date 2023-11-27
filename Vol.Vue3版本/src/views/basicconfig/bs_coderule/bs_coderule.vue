<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/basicconfig/bs_coderule/bs_coderule.js此处编写
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
    import extend from "@/extension/basicconfig/bs_coderule/bs_coderule.js";
    import { ref, defineComponent } from "vue";
    export default defineComponent({
        setup() {
            const table = ref({
                key: 'coderule_id',
                footer: "Foots",
                cnName: '编码规则',
                name: 'bs_coderule/bs_coderule',
                url: "/bs_coderule/",
                sortName: "coderule_id"
            });
            const editFormFields = ref({"fromcode":"","prefix":"","time":"","serialnumber":"","generative":""});
            const editFormOptions = ref([[{"title":"编号前缀","field":"prefix","type":"text"},
                               {"dataKey":"编码规则目标表单","data":[],"title":"目标表单","required":true,"field":"fromcode","type":"select"}],
                              [{"dataKey":"编码规则提交时间","data":[],"title":"时间规则","required":true,"field":"time","type":"radio"},
                               {"title":"流水号位","required":true,"field":"serialnumber","type":"text"}],
                              [{"title":"编号生成规则","field":"generative","type":"text"}]]);
            const searchFormFields = ref({"fromcode":""});
            const searchFormOptions = ref([[{"dataKey":"编码规则目标表单","data":[],"title":"目标表单","field":"fromcode","type":"select"}]]);
            const columns = ref([{field:'coderule_id',title:'编号规则主键',type:'int',width:110,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'generative',title:'编号生成规则',type:'string',link:true,width:110,align:'left',sort:true},
                       {field:'fromcode',title:'目标表单',type:'string',bind:{ key:'编码规则目标表单',data:[]},width:110,require:true,align:'left'},
                       {field:'prefix',title:'编号前缀',type:'string',width:110,align:'left'},
                       {field:'time',title:'时间规则',type:'string',bind:{ key:'编码规则提交时间',data:[]},width:110,require:true,align:'left'},
                       {field:'serialnumber',title:'流水号位',type:'int',width:110,require:true,align:'left'},
                       {field:'CreateID',title:'创建者ID',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:100,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'datetime',width:150,align:'left',sort:true},
                       {field:'ModifyID',title:'更新者ID',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Modifier',title:'更新者',type:'string',width:100,align:'left'},
                       {field:'ModifyDate',title:'更新时间',type:'datetime',width:150,align:'left',sort:true}]);
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
