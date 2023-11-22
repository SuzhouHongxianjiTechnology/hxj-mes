<!--
 * @Description: 首页
 * @Author: AlanGao
 * @Date: 2023-11-12 21:40:02
 * @LastEditors: AlanGao
 * @LastEditTime: 2023-11-22 02:18:16
-->
<template>
<scaleBoxVue>
<div class="container">
    <!-- header -->
    <div class="header">
      <dv-decoration5 :dur="2" style="width:100%;height:100%;" />
      <span class="center_title">MES 首页</span>
    </div>
    <div class="main">
      <el-row class="mianRow">
        <el-col :span="6" class="leftCol">
          <div class="leftUnifiedStyle">
            <dv-border-box12 class="borderBox12" style="width:100%;">
              <div class="borderContent" id="weekEchart"></div>
            </dv-border-box12>
          </div>
          <div class="leftUnifiedStyle">
            <dv-border-box12 class="borderBox12" style="width:50%;">
              <div class="borderContent" id="processEchart">
              </div>
            </dv-border-box12>
            <dv-border-box12 class="borderBox12" style="width:50%;">
              <div class="borderContent">
                <div class="borderDiv">当日生产率评估</div>
                <div style="height: calc(50% - 16px)">
                  123
                </div>
                <div style="height: calc(50% - 16px)">12</div>
              </div>
            </dv-border-box12>
          </div>
          <div class="leftUnifiedStyle">
            <dv-border-box12 class="borderBox12" style="width:100%;">
              <div class="borderContent" id="scheduleEchart"></div>
            </dv-border-box12>
          </div>
        </el-col>
        <el-col :span="12" class="centerCol">
          <div style="height: 33%;display: flex;">
            <dv-border-box12 class="borderBox12" style="width:50%;">
              <div  class="borderContent" style="display:flex">
                <div style="width:50%;height:100%" id="overallOrderEchart"></div>
                <div style="width:50%;">
                  123
                </div>
              </div>
            </dv-border-box12>
            <dv-border-box12 class="borderBox12" style="width:50%;">
              <div  class="borderContent" style="display:flex">
                <div style="width:50%;height:100%" id="todayOrderEchart"></div>
                <div style="width:50%;">
                  123
                </div>
              </div>
            </dv-border-box12>
          </div>
          <div style="height: 66%">
            <dv-border-box12 class="borderBox12" style="width:100%;">
              <div class="borderContent">
                <div class="borderDiv">近期生产计划列表</div>
                <el-table :data="tableData" border style="width: 100%" class="borderEcharts">
                  <el-table-column prop="date" label="Date" />
                  <el-table-column prop="name" label="Name" />
                  <el-table-column prop="address" label="Address" />
                </el-table>
              </div>
            </dv-border-box12>
          </div>
        </el-col>
        <el-col :span="6" class="rightCol">
          <div style="height: 33%;display: flex;">
            <dv-border-box12 class="borderBox12" style="width:50%;">
              123
            </dv-border-box12>
            <dv-border-box12 class="borderBox12" style="width:50%;">
              333
            </dv-border-box12>
          </div>
          <div style="height: 66%">
            <dv-border-box12 class="borderBox12" style="width:100%;">
              <div class="borderContent">
                <div class="borderDiv">最近生产产品列表</div>
                <el-table :data="tableData" border style="width: 100%" class="borderEcharts">
                  <el-table-column prop="date" label="Date" />
                  <el-table-column prop="name" label="Name" />
                  <el-table-column prop="address" label="Address" show-overflow-tooltip/>
                </el-table>
              </div>
            </dv-border-box12>
          </div>
        </el-col>
      </el-row>
    </div>
  </div>
</scaleBoxVue>
  
</template>

<script setup>
  import * as echarts from 'echarts'
  import { onBeforeUnmount, onMounted,ref } from 'vue'
  import scaleBoxVue from '../components/scaleBox.vue'
  /**
   * @description: 获取一周不良统计图表数据
   */  
  const getweekEcharts = () => {
    const weekEchart = echarts.init(document.getElementById('weekEchart'))
    // 一周不良统计数据
    let weekOption = {
      title: {
        text: "一周不良统计",
        textStyle: {
          fontSize: 24
        },
      },
      grid: {
        bottom: '10%'
      },
      xAxis: {
        type: 'category',
        boundaryGap: false,
        data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
      },
      yAxis: {
        type: 'value'
      },
      series: [
        {
          data: [820, 932, 901, 934, 1290, 1330, 1320],
          type: 'line',
          areaStyle: {}
        }
      ]
    }
    weekEchart.setOption(weekOption)
  }
  /**
   * @description: 获取关键工序数据
   */  
  const getProcessEcharts = () => {
    const processEchart = echarts.init(document.getElementById('processEchart'))
    // 关键工序
    let processOption = {
      title: {
        text: "关键工序",
        textStyle: {
          fontSize: 24
        },
      },
      grid: {
        bottom: '10%'
      },
      radar: {
        // shape: 'circle',
        indicator: [
          { name: 'Sales', max: 6500 },
          { name: 'Administration', max: 16000 },
          { name: 'Information Technology', max: 30000 },
          { name: 'Customer Support', max: 38000 },
          { name: 'Development', max: 52000 },
          { name: 'Marketing', max: 25000 }
        ]
      },
      series: [
        {
          name: 'Budget vs spending',
          type: 'radar',
          areaStyle:{
            color: "rgba(16,72,76,.5)"
          },
          data: [
            {
              value: [4200, 3000, 20000, 35000, 50000, 18000],
              name: 'Allocated Budget'
            }
          ]
        }
      ]
    }
    processEchart.setOption(processOption)
  }
  /**
   * @description: 获取一周产量均差
   */  
  const getScheduleEcharts = () => {
    const scheduleEchart = echarts.init(document.getElementById('scheduleEchart'))
    // 一周不良统计数据
    let scheduleOption = {
      title: {
        text: "一周产量均差",
        textStyle: {
          fontSize: 24
        },
      },
      grid: {
        bottom: '10%'
      },
      xAxis: {
        type: 'category',
        boundaryGap: false,
        data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
      },
      yAxis: {
        type: 'value'
      },
      series: [
        {
          data: [820, 932, 901, 934, 1290, 1330, 1320],
          type: 'line',
          areaStyle: {}
        }
      ]
    }
    scheduleEchart.setOption(scheduleOption)
  }
  /**
   * @description: 总体订单进度
   */  
  const getOverallOrderEcharts = () => {
    const orderEchart = echarts.init(document.getElementById('overallOrderEchart'))
    // 一周不良统计数据
    let orderOption = {
      grid: {
        bottom: '10%'
      },
      tooltip: {
        trigger: 'item'
      },
      series: [
        {
          name: '订单总计划',
          type: 'pie',
          radius: ['40%', '70%'],
          avoidLabelOverlap: false,
          label: {
            show: false,
            position: 'center'
          },
          tooltip: {
            formatter: '{a} <br />{b}: {d}%'
          },
          emphasis: {
            label: {
              show: true,
              fontSize: 20,
              formatter: '{b}',
              fontWeight: 'bold'
            }
          },
          labelLine: {
            show: false
          },
          data: [
            { value: 1048, name: 'Search Engine' },
            { value: 735, name: 'Direct' },
          ]
        }
      ]
    }
    orderEchart.setOption(orderOption)
  }
  /**
   * @description: 订单今日进度
   */  
  const getTodayOrderEcharts = () => {
    const orderEchart = echarts.init(document.getElementById('todayOrderEchart'))
    // 一周不良统计数据
    let orderOption = {
      grid: {
        bottom: '10%'
      },
      tooltip: {
        trigger: 'item'
      },
      series: [
        {
          name: '订单总计划',
          type: 'pie',
          radius: ['40%', '70%'],
          avoidLabelOverlap: false,
          label: {
            show: false,
            position: 'center'
          },
          tooltip: {
            formatter: '{a} <br />{b}: {d}%'
          },
          // emphasis: {
          //   label: {
          //     show: true,
          //     fontSize: 20,
          //     formatter: '{b}',
          //     fontWeight: 'bold'
          //   }
          // },
          labelLine: {
            show: false
          },
          data: [
            { value: 1048, name: 'Search Engine'},
            { value: 735, name: 'Direct'},
          ]
        }
      ]
    }
    orderEchart.setOption(orderOption)
  }
  /**
   * @description: 近期生产计划列表
   */  
  const tableData = [
    {
      date: '2016-05-03',
      name: 'Tom',
      address: 'No. 189, Grove St, Los Angeles',
    },
    {
      date: '2016-05-02',
      name: 'Tom',
      address: 'No. 189, Grove St, Los Angeles',
    },
    {
      date: '2016-05-04',
      name: 'Tom',
      address: 'No. 189, Grove St, Los Angeles',
    },
    {
      date: '2016-05-01',
      name: 'Tom',
      address: 'No. 189, Grove St, Los Angeles',
    },
    {
      date: '2016-05-03',
      name: 'Tom',
      address: 'No. 189, Grove St, Los Angeles',
    },
    {
      date: '2016-05-02',
      name: 'Tom',
      address: 'No. 189, Grove St, Los Angeles',
    },
    {
      date: '2016-05-04',
      name: 'Tom',
      address: 'No. 189, Grove St, Los Angeles',
    },
    {
      date: '2016-05-01',
      name: 'Tom',
      address: 'No. 189, Grove St, Los Angeles',
    },
    {
      date: '2016-05-03',
      name: 'Tom',
      address: 'No. 189, Grove St, Los Angeles',
    },
    {
      date: '2016-05-02',
      name: 'Tom',
      address: 'No. 189, Grove St, Los Angeles',
    },
    {
      date: '2016-05-04',
      name: 'Tom',
      address: 'No. 189, Grove St, Los Angeles',
    },
    {
      date: '2016-05-01',
      name: 'Tom',
      address: 'No. 189, Grove St, Los Angeles',
    },
  ]
  onMounted(() => {
    getweekEcharts()
    getProcessEcharts()
    getScheduleEcharts()
    getOverallOrderEcharts()
    getTodayOrderEcharts()
  })
  onBeforeUnmount(() => {

  })
</script>

<style lang="less" scoped>
// 单元格不换行
::v-deep.el-table td.el-table__cell div {
  white-space: nowrap !important;
}
.el-table {
  overflow: hidden;
}
// 小标题样式
.borderDiv {
  height: 32px;
  font-size: 1.5rem;
  text-align: center;
}
// 表或者图标的高度
.borderEcharts {
  height:calc(100% - 32px);
}
.borderBox12{
  height:100%;
  padding: 10px;
  .borderContent {
    height: 100%;
    width: 100%
  }
}
.container {
  height: 100%;
  width: 100%;
  // 头部样式
  .header{
    height: 8%;
    width: 100%;
    position: relative;
    .center_title {
      position: absolute;
      top: 34%;
      left: 50%;
      transform: translate(-50%,-50%);
      font-size: 1.5rem;
      font-weight: 700;
    }
  }
  // 底部样式
  .main {
    height: 92%;
    .mianRow {
      height: 100%;
      .leftCol,.centerCol,.rightCol {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
      }
      .el-col {
        height: 100%;
        padding: 0px 10px;
      }
      // 底部左侧div统一样式
      .leftUnifiedStyle {
        height: 33%;
        display: flex;
      }
    }
  }
}
</style>