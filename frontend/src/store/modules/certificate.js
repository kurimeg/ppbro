const state = {
  certificates: []
}

const mutations = {
  setCertificates (state, certificates) {
    state.certificates = certificates
  }
}

const actions = {
  fetchCertificates ({ commit }) {
    const certificates = [
      { issuerName: '法政大学', issueDate: '2015/03/01', detail: 'キャリアデザイン学部キャリアデザイン学科', type: '卒業' },
      { issuerName: '株式会社システムコンサルタント', issueDate: '2015/04/01', detail: 'オープンシステム統括部', type: '入社' },
      { issuerName: '情報処理推進機構', issueDate: '2016/10/31', detail: '基本情報技術者', type: '取得' },
      { issuerName: '情報処理推進機構', issueDate: '2016/10/31', detail: '基本情報技術者', type: '取得' }
    ]
    commit('setCertificates', certificates)
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
