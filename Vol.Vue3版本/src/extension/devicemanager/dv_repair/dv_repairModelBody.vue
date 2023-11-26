<template>
  <vol-box
    :lazy="true"
    v-model="model"
    title="选择设备"
    :height="500"
    :width="1200"
    :padding="5"
    :onModelClose="onModelClose"
  >
    <machinery ref="machinery"></machinery>
    <template #footer>
      <div>
        <el-button type="primary" size="small" @click="confirm">确认</el-button >
        <el-button type="default" size="small" @click="onModelClose" >关闭</el-button >
      </div></template>
  </vol-box>
</template>
<script>
import machinery from "@/views/devicemanager/dv_machinery/dv_machinery.vue"
import VolBox from '@/components/basic/VolBox.vue';
export default {
  components: { 'vol-box': VolBox,machinery },
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
            $parent.editFormFields.machinery_code = row[0].machinery_code;
            $parent.editFormFields.machinery_name = row[0].machinery_name;  
        })
        this.model = false;
    }
  }
};
</script>