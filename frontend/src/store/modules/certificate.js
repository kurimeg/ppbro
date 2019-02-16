const state = {
  certificates: []
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
  fetchCertificates ({ commit }) {
    const certificates = [
      { issuerName: '法政大学', issueDate: '2015/03/01', detail: 'キャリアデザイン学部キャリアデザイン学科', type: '卒業', issuerSign: true, userSign: true },
      { issuerName: '株式会社システムコンサルタント', issueDate: '2015/04/01', detail: 'オープンシステム統括部', type: '入社', issuerSign: true, userSign: true },
      { issuerName: '情報処理推進機構', issueDate: '2016/10/31', detail: '基本情報技術者', type: '取得', issuerSign: true, userSign: true },
      { issuerName: '情報処理推進機構', issueDate: '2016/10/31', detail: '基本情報技術者', type: '取得', issuerSign: false, userSign: false }
    ]
    commit('setCertificates', certificates)
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
