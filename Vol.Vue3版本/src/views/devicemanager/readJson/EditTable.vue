<template>
  <div class="edit-table-container">
    <!-- {{stationIndex}} -->
    <div class="table">
      <div class="table-title">
        <i class="el-icon-postcard"></i>
        ReadData
      </div>
      <div class="table-operate">
        <el-button type="primary" @click="addLine">
          <i class="el-icon-plus"></i>
        </el-button>
      </div>
    </div>
    <el-table :data="dataList" style="width: 100%">
      <el-table-column v-for="(item, index) in tableHeader" :key="item" :prop="item" :label="item">
        <template #default="scope">
          <div>
            <template v-if="item === 'SeqId' ? scope.row[item] = stationId : ''" ></template>
            <el-input
              v-model="scope.row[item]"
              :disabled="item === 'Id' || item === 'SeqId'"
            />
          </div>
        </template>
      </el-table-column>
      <el-table-column prop="del" label="" width="70">
        <template #default="scope">
          <el-button type="primary" @click="deleteLine(scope.$index)">
            <i class="el-icon-delete"></i>
          </el-button>
          <!-- <span><i class="el-icon-delete"></i></span> -->
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>
<script setup>
import { ref, onMounted } from 'vue'
const { dataList, deviceIndex, stationIndex, stationId } =  defineProps({
  dataList: Array,
  deviceIndex: Number,
  stationIndex: Number,
  stationId: String,
})
const tableHeader = ref(['Id', 'SeqId', 'Sort', 'SqlColumnName', 'TypeAndDb'])
const addLine = () => {
  console.log(dataList)
  let lastId = dataList.length ? dataList[dataList.length - 1].Id : '0'
  let newId = (Number(lastId) + 1).toString()
  dataList.push({
    Id: newId,
    SeqId: stationId.toString(),
    Sort: newId,
    SqlColumnName: '',
    TypeAndDb: ''
  })
}
const deleteLine = (index) => {
  console.log(index)
  dataList.splice(index, 1)
}
// defineExpose({changeData})
onMounted(() => {
  // dataList.forEach((ele) => {
  //   ele.SeqId = stationId.toString(),
  // })
})
</script>
<style lang="less" scoped>
.edit-table-container {
  .table {
    display: flex;
    justify-content: space-between;
    .table-title {
      color: #606266;
    // padding-right: 10px;
    }
    .table-operate {
      margin-right: 12px;
    }
  }
}
</style>