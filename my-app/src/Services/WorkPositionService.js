import http from "../http";
class WorkPositionService {
  getAllPositions() {
    return http.get("/getWorkPositions");
  }
  createNewWorkPosition(workPositionModel) {
    return http.post("/createWorkPosition", JSON.stringify(workPositionModel));
  }
  deleteWorkPosition(workPositionModel) {
    return http.post("/deleteWorkPosition", JSON.stringify(workPositionModel));
  }
  getHistoryOfWorkPositions(employeeId) {
    return http.post("/getHistoryOfWorkPositions", JSON.stringify(employeeId));
  }

}
export default new WorkPositionService();