<!--
 * @Description: 首页
 * @Author: AlanGao
 * @Date: 2023-11-12 21:40:02
 * @LastEditors: AlanGao
 * @LastEditTime: 2023-11-19 00:18:39
-->
<template>
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
              <div class="borderContent">
                <div class="borderDiv">一周不良统计</div>
                <dv-charts :option="weekOption" class="borderEcharts"/>
              </div>
            </dv-border-box12>
          </div>
          <div class="leftUnifiedStyle">
            <dv-border-box12 class="borderBox12" style="width:50%;">
              <div class="borderContent">
                <div class="borderDiv">关键工序</div>
                <dv-charts :option="processOption" class="borderEcharts"/>
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
              <div class="borderContent">
                <div class="borderDiv">关键工序</div>
                <dv-charts :option="weekOption" class="borderEcharts"/>
              </div>
            </dv-border-box12>
          </div>
        </el-col>
        <el-col :span="12" class="centerCol">
          <div style="height: 33%;display: flex;">
            <dv-border-box12 class="borderBox12" style="width:50%;">
              <div  class="borderContent" style="display:flex">
                <dv-charts :option="scheduleOption" style="width:50%;height:100%"/>
                <div style="width:50%;">
                  123
                </div>
              </div>
            </dv-border-box12>
            <dv-border-box12 class="borderBox12" style="width:50%;">
              <div  class="borderContent" style="display:flex">
                <dv-charts :option="scheduleOption" style="width:50%;height:100%"/>
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
                <dv-scroll-board :config="tableConfig" class="borderEcharts" />
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
                <dv-scroll-board :config="tableConfig" class="borderEcharts" />
              </div>
            </dv-border-box12>
          </div>
        </el-col>
      </el-row>
    </div>
  </div>
</template>

<script>
import * as echarts from 'echarts'
export default {
  data() {
    return {
      weekOption: {
        title: {
          text: "一周不良统计"
        },
        grid: {
          top: '5%',
          bottom: '10%'
        },
        xAxis: {
          data: ['1', '2', '3', '4', '5', '6'],
          boundaryGap: false
        },
        yAxis: {
          data: 'value',
          interval: 0.2,
          min: 0,
          max: 1
        },
        series: [
          {
            data: [0, 0, 0, 1, 1, 0],
            type: 'line',
            // 设置填充
            lineArea: {
              show: true,
              gradient: ['rgba(55, 162, 218, 0.6)', 'rgba(55, 162, 218, 0)']
            }
          }
        ]
      },
      processOption:{
        grid: {
          left: 10,
          right: 10
        },
        radar: {
          indicator: [
            { name: '西峡', max: 300 },
            { name: '周口', max: 300 },
            { name: '南阳', max: 300 },
            { name: '驻马店', max: 300 },
            { name: '郑州', max: 300 },
            { name: '洛阳', max: 300 }
          ]
        },
        series: [
          {
            type: 'radar',
            data: [111, 256, 178, 152, 266, 132]
          }
        ]
      },
      scheduleOption: {
        grid: {
          left:'2%',
          right: '2%'
        },
        series: [
          {
            type: 'pie',
            data: [
              { name: '可口可乐', value: 93 },
              { name: '百事可乐', value: 32 },
            ],
            radius: ['30%', '50%'],
            insideLabel: {
              show: true,
              formatter: '{name}'
            },
            outsideLabel: {
              show: false
            }
          }
        ]
      },
      tableConfig: {
        header: ['列1', '列2', '列3'],
        data: [
          ['行1列1', '行1列2', '行1列3'],
          ['行2列1', '行2列2', '行2列3'],
          ['行3列1', '行3列2', '行3列3'],
          ['行4列1', '行4列2', '行4列3'],
          ['行5列1', '行5列2', '行5列3'],
          ['行6列1', '行6列2', '行6列3'],
          ['行7列1', '行7列2', '行7列3'],
          ['行8列1', '行8列2', '行8列3'],
          ['行9列1', '行9列2', '行9列3'],
          ['行10列1', '行10列2', '行10列3'],
        ],
        align: ['center'],
        headerBGC: 'transparent',
        oddRowBGC: 'transparent',
        evenRowBGC: 'transparent',
        waitTime: 1000,
        rowNum: 8
      },
      tableData: [
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
    }
  },
  mounted() {
  },
  methods: {
  }
}

</script>

<style lang="less" scoped>
// 小标题样式
.borderDiv {
  height: 32px;
  font-size: 1.5rem;
  text-align: center;
}
// 表或者图标的高度
.borderEcharts {
  height:calc(100% - 32px)
}
.borderBox12{
  height:100%;
  padding: 10px;
  .borderContent {
    height: 100%;
    width: 100%
  }
}
// ::v-deep.border-box-content {
//     display:flex;
//   }
.container {
  height: calc(100vh - 100px);
  width: 100%;
  background-color: skyblue;
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
      // color: #fff;
      font-size: 1.5rem;
    }
  }
  // 底部样式
  .main {
    height: 92%;
    // border: 1px solid red;
    .mianRow {
      height: 100%;
      // color: #fff;
      .leftCol,.centerCol,.rightCol {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
      }
      .el-col {
        height: 100%;
        border-right: 1px solid #fff;
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