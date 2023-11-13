/*
 * @Description: 创建rem.js
 * @Author: AlanGao
 * @Date: 2023-11-14 00:11:06
 * @LastEditors: AlanGao
 * @LastEditTime: 2023-11-14 00:15:35
 */
// 基准大小
const baseSize = 16
function setRem() {
    // 当前页面宽度相对于1920宽度的缩放比例
    const scale = document.documentElement.clientWidth / 1920
    // 设置页面根节点字体大小，最高放大到2
    document.documentElement.style.fontSize = baseSize * Math.min(scale,2) + 'px'
}
setRem()
window.onresize = function() {
    setRem()
}