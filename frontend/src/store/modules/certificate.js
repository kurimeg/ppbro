import apiClient from '@/api/client.js'

const state = {
  certificates: [],
  profiles: JSON.parse(localStorage.getItem('profiles')) || []
}

const mutations = {
  setCertificates (state, certificates) {
    state.certificates = certificates
  },
  addCertificate (state, certificate) {
    state.certificates.push(certificate)
  }
}

const actions = {
  fetchProofs ({ commit }) {
    apiClient.fetchProofs(state.profiles).then(respons => {
      console.log(respons)
      const proofs = respons.proofs.map(item => {
        return {
          issuerName: item.OrgName,
          issueDate: item.DateTime,
          detail: '',
          type: item.Value,
          issuerSign: item.Verified,
          userSign: ''
        }
      })
      commit('setCertificates', proofs)
    })
  },
  requestCertificate ({ commit }) {
    commit('addCertificate', { issuerName: '情報処理推進機構', issueDate: '2016/10/31', detail: '基本情報技術者', type: '取得', issuerSign: false, userSign: false })
    return new Promise((resolve, reject) => {
      resolve()
    })
  },
  shareCertificate ({ commit }) {
    return new Promise((resolve, reject) => {
      resolve()
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
