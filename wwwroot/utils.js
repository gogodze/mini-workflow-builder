window.getBoundingClientRect = (el) => {
  return el.getBoundingClientRect();
};

window.setDragNodeType = function (e, type) {
  e.dataTransfer.setData("external-node-type", type);
}

window.getDragData = function (e, type) {
  return e.dataTransfer.getData(type);
};

