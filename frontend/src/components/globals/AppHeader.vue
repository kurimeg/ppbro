<template>
  <div>
    <v-toolbar color="primary" dark fixed app>
      <v-toolbar-title>{{ headerName }}</v-toolbar-title>
    </v-toolbar>
  </div>
</template>

<script>
export default {
  data: () => ({
    navigations: [
      { icon: 'account_circle', name: 'ポートフォリオ', route: { name: 'portfolio' } },
      { icon: 'edit', name: 'プロフィール編集', route: { name: 'profile' } }
    ]
  })
  ,
  props: {
    headerName: String
  }
}
</script>

<style>

</style>
