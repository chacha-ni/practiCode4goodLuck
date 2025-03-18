import axios from 'axios';

const apiUrl = "http://localhost:5000"

export default {
  getTasks: async () => {
    const result = await axios.get(`${apiUrl}/`)    
    return result.data;
  },

  addTask: async(name)=>{
    console.log('addTask', name)
    //TODO
    axios.post(`${apiUrl}/${name}`)
    return {};
  },

  setCompleted: async(id, isComplete)=>{
    console.log('setCompleted', {id, isComplete})
    //TODO
    axios.put(`${apiUrl}/${id}`, {isComplete})
    return {};
  },

  deleteTask:async(id)=>{
    console.log('deleteTask')
    axios.delete(`${apiUrl}/${id}`)
  }
};
