<!--
 * @Description: 易损件借还页弹框编码的高级选择页面
 * @Author: AlanGao
 * @Date: 2023-12-02 16:53:48
 * @LastEditors: AlanGao
 * @LastEditTime: 2023-12-02 17:13:07
-->
<template>
  <vol-box
    :lazy="true"
    v-model="model"
    title="选择易损件"
    :height="500"
    :width="1200"
    :padding="5"
    :onModelClose="onModelClose"
  >
    <tmTool ref="machinery"></tmTool>
    <template #footer>
      <div>
        <el-button type="primary" size="small" @click="confirm">确认</el-button >
        <el-button type="default" size="small" @click="onModelClose" >关闭</el-button >
      </div></template>
  </vol-box>
</template>
<script>
import tmTool from "@/views/devicemanager/tm_tool/tm_tool.vue"
import VolBox from '@/components/basic/VolBox.vue';
export default {
  components: { 'vol-box': VolBox,tmTool },
  methods: {},
  data() {
    return {
      model: false
    };
  },
  methods: {
    onModelClose(){
         this.model = false
    },
    openDevice() {
        this.model = true;
    },
    confirm() {
        let row = this.$refs.machinery.$refs.grid.getSelectRows();
        this.$emit('parentCall', $parent => { 
            $parent.editFormFields.tool_code = row[0].tool_code;
            $parent.editFormFields.tool_name = row[0].tool_name; 
        })
        this.model = false;
    }
  }
};
</script>